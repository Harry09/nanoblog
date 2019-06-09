import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/api/account_api.dart';
import 'package:nanoblog/api/entry_api.dart';
import 'package:nanoblog/exceptions/api_exception.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/screens/add_post.dart';
import 'package:nanoblog/screens/login.dart';
import 'package:scoped_model/scoped_model.dart';
import 'package:shared_preferences/shared_preferences.dart';

class HomePage extends StatefulWidget
{
  @override
  State<StatefulWidget> createState() => HomePageState();
}

class HomePageState extends State<HomePage>
{
  final _scaffoldKey = GlobalKey<ScaffoldState>();
  List<Entry> entries;
  AppStateModel model;

  HomePageState()
  {
    entries = List<Entry>();
    // mockData();
  }

  @override
  void initState()
  {
    super.initState();

    SchedulerBinding.instance.addPostFrameCallback((_) {
      refreshData();
    });
  }

  void _showSnackBar(String message)
  {
    _scaffoldKey.currentState.showSnackBar(SnackBar(
      content: Text(message),
    ));
  }

  Widget _buildPostHeader(Entry entry)
  {
    return 
    Container(
      padding: EdgeInsets.symmetric(vertical: 10),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            padding: EdgeInsets.symmetric(horizontal: 5),
            child: Text(
              entry.author.userName,
              style: TextStyle(
                fontWeight: FontWeight.bold
              )
            ),
          ),
          Text(entry.createTime)
        ],
      )
    );
  }

  Widget _buildPostBody(Entry entry)
  {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 5),
      child: Align(
        alignment: Alignment.centerLeft,
        child: Flex(
          direction: Axis.vertical,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Text(entry.text)
          ],
        )
      )
    );
  }

  Future _deletePost(Entry entry) async
  {
    if (model.jwtService.jwtToken == null)
      return;

    model.jwtService.tryRefreshToken();

    try
    {
      if (await EntryApi.deleteEntry(entry.id, model.jwtService.jwtToken))
      {
        refreshData();
      }
      else
      {
        _showSnackBar("Cannot remove post!");
      }

    }
    on ApiException catch (ex)
    {
      _showSnackBar(ex.toString());
    }
  }

  void entryMoreOptions(Entry entry)
  {
    List<Widget> columnItems;

    if (model.currentUser != null && model.currentUser.id == entry.author.id)
    {
      columnItems = [
        ListTile(
          leading: Icon(Icons.delete),
          title: Text("Delete entry"),
          onTap: () {
            Navigator.pop(_scaffoldKey.currentContext);
            _deletePost(entry);
          }
        )
      ];
    }
    else
    {
      columnItems = [
        Center(
          child: Padding(
            padding: EdgeInsets.all(16),
            child: Text(
              "Nothing here :(",
              style: TextStyle(
                fontSize: 24
              ),
          ),
          )
        )
      ];
    }

    showModalBottomSheet(
      context: _scaffoldKey.currentContext,
      builder: (BuildContext context) {
        return Container(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: columnItems
          ),
        );
      }
    );
  }

  Widget _buildPostFooter(Entry entry)
  {
    return Align(
      alignment: Alignment.centerRight,
      child: IconButton(
        icon: Icon(Icons.more_vert),
        onPressed: () => entryMoreOptions(entry),
        padding: EdgeInsets.all(0),
      ),
    );
  }

  Widget _buildPost(Entry entry)
  {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        border: Border.all(color: Colors.white),
        borderRadius: BorderRadius.all(Radius.circular(10))
      ),
      padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
      margin: EdgeInsets.symmetric(vertical: 5),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          _buildPostHeader(entry),
          _buildPostBody(entry),
          _buildPostFooter(entry)
        ],
      )
    );
  }

  Widget _buildBody()
  {
    if (entries == null)
    {
      return Container(
        padding: EdgeInsets.all(10),
        child: Text("Nothing here :(")
      );
    }    

    return Container(
      color: Colors.grey[200],
      child: Center(
        child: RefreshIndicator(
          onRefresh: refreshData,
          child: ListView.builder(
            padding: EdgeInsets.all(10),
            itemCount: entries.length,
            itemBuilder: (BuildContext ctxt, int index) => _buildPost(entries[index])
          ),
        ), 
      )
    );
  }

  Future refreshData() async
  {
    final entries = await EntryApi.getEntries();
    setState(() {
      this.entries = entries;
    });
  }

  void addPost() async
  {
    var result = await Navigator.push<bool>(context, MaterialPageRoute(
        builder: (context) => AddPostPage()
      )
    );

    if (result == null)
      return;

    if (result)
    {
      _showSnackBar("Post added!");
      refreshData();
    }
    else
    {
      _showSnackBar("Something went wrong :/");
    }
  }

  Future logoff() async
  {
    await model.jwtService.resetToken();
    
    setState(() {
      model.currentUser = null;
    });

    Navigator.pushNamedAndRemoveUntil(_scaffoldKey.currentContext, "/login", (_) => false);
  }

  Future showProfileOptions() async
  {
    var username;

    if (model.currentUser != null)
    {
      username = model.currentUser.userName;
    }

    showModalBottomSheet(
      context: _scaffoldKey.currentContext,
      builder: (BuildContext context) {
      return Container(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            Center(
              child: Padding(
                padding: EdgeInsets.all(16),
                child: Text(
                  username,
                  style: TextStyle(
                    fontSize: 24
                  ),
                )
              )
            ),
            ListTile(
              leading: new Icon(Icons.exit_to_app),
              title: new Text('Log off'),
              onTap: () {
                Navigator.pop(_scaffoldKey.currentContext);
                logoff();
              },
            )
          ],
        )
      );
    });
  }

  @override
  Widget build(BuildContext context)
  {
    model = ScopedModel.of<AppStateModel>(context, rebuildOnChange: true);

    Widget loginWidget;

    if (model.currentUser != null)
    {
      loginWidget = FlatButton(
        child: Text(model.currentUser.userName),
        onPressed: showProfileOptions,
      );
    }

    return Scaffold(
      key: _scaffoldKey,
      appBar: AppBar(
        title: Text("Nanoblog"),
        actions: <Widget>[
          IconButton(
            icon: Icon(Icons.refresh),
            onPressed: refreshData,
          ),
          loginWidget
        ],
      ),
      floatingActionButton: FloatingActionButton(
        child: Icon(Icons.add),
        onPressed: addPost,
      ),
      body: _buildBody(),
    );
  }
}
