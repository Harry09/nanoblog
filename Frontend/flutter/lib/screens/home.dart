import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/api/entry_api.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/add_post.dart';
import 'package:nanoblog/widgets/entry_list.dart';
import 'package:scoped_model/scoped_model.dart';

class HomePage extends StatefulWidget
{
  @override
  State<StatefulWidget> createState() => HomePageState();
}

class HomePageState extends State<HomePage>
{
  final _scaffoldKey = GlobalKey<ScaffoldState>();
  final _entryListKey = GlobalKey<EntryListState>();
  AppStateModel _model;

  HomePageState();

    @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of<AppStateModel>(context, rebuildOnChange: true);

    Widget loginWidget;

    if (_model.currentUser != null)
    {
      loginWidget = IconButton(
        icon: Icon(Icons.person),
        onPressed: _showProfileOptions,
      );
    }
    else
    {
      loginWidget = FlatButton(
        child: Text("Login"),
        onPressed: () {
          Navigator.pushNamedAndRemoveUntil(context, "/login", (_) => false);
        },
      );
    }

    return Scaffold(
      key: _scaffoldKey,
      appBar: AppBar(
        title: Text("Nanoblog"),
        actions: <Widget>[
          loginWidget
        ],
      ),
      floatingActionButton: FloatingActionButton(
        child: Icon(Icons.add),
        onPressed: _addPost,
      ),
      body: EntryList(
          key: _entryListKey, 
          loader: EntryApi.getEntries
        ),
    );
  }

  void _addPost() async
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
        action: SnackBarAction(
          label: "Show",
          onPressed: () {}, // TODO: show post
        ),
      ));

      _entryListKey.currentState.reloadEntries();
    }
    else
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Something went wrong :/"),
      ));
    }
  }

  Future logoff() async
  {
    await _model.jwtService.resetToken();
    
    setState(() {
      _model.currentUser = null;
    });

    Navigator.pushNamedAndRemoveUntil(_scaffoldKey.currentContext, "/login", (_) => false);
  }

  Future _showProfileOptions() async
  {
    var username;

    if (_model.currentUser != null)
    {
      username = _model.currentUser.userName;
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
}
