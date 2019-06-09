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
      tryLogin();
      refreshData();
    });
  }

  void tryLogin() async
  {
    await model.jwtService.load();

    if (model.jwtService.jwtToken != null)
    {
      await model.jwtService.tryRefreshToken();

      if (model.jwtService.isExpired())
        return;

      var user = await AccountApi.getUser(model.jwtService.jwtToken.getUserId());

        if (user != null)
        {
          setState(() {
            model.currentUser = user;
          });
        }
    }
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

  Widget _buildPostFooter(Entry entry)
  {

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
          //_buildPostFooter()
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
    var entries_ = await EntryApi.getEntries();
    setState(() {
      entries = entries_;
    });
  }

  // void mockData()
  // {
  //   var defaultUser = User(
  //     userName: "Harry"
  //   );

  //   entries.add(Entry(
  //     author: defaultUser,
  //     createTime: "10 hours ago",
  //     text: "Sint commodo proident pariatur in qui ea non. Anim aute culpa duis non sunt incididunt laborum nisi tempor."
  //   ));

  //   entries.add(Entry(
  //     author: defaultUser,
  //     createTime: "5 hours ago",
  //     text: "zażółć gęślą jaźń"
  //   ));
  // }

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
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Post added!"),
      ));

      refreshData();
    }
    else
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Something went wrong :/"),
      ));
    }
  }

  Future login() async
  {
    try
    {
      var result = await AccountApi.login("email@email.email", "password");

      if (result != null)
      {
        model.jwtService.setJwt(result);

        var user = await AccountApi.getUser(result.getUserId());

        if (user != null)
        {
          setState(() {
            model.currentUser = user;
          });
        }
      }
    }
    on ApiException catch(ex)
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text(ex.toString()),
      ));
    }
  }

  Future logoff() async
  {
    await model.jwtService.resetToken();
    
    setState(() {
      model.currentUser = null;
    });

  }

  Future showProfileOptions() async
  {
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
                child:  Text(
                  model.currentUser.userName,
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

    if (model.currentUser == null)
    {
      loginWidget = IconButton(
        icon: Icon(Icons.person),
        onPressed: login
      );
    }
    else
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
