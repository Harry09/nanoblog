import 'package:flutter/material.dart';
import 'package:nanoblog/api/entry_api.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/add_post.dart';
import 'package:nanoblog/screens/entry_detail.dart';
import 'package:nanoblog/widgets/entry/entry_list.dart';
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

  int _loaderPage = 0;

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
          loader: _entryListLoader,
          extraLoader: _entryListExtraLoader,
        ),
    );
  }

  Future<List<Entry>> _entryListLoader() async
  {
    _loaderPage = 0;

    await _model.jwtService.tryRefreshToken();

    return await EntryApi.getNewest(
      pagedQuery: PagedQuery(
        currentPage: 0,
        limitPerPage: 10
      ),
      jwtToken: _model.jwtService.jwtToken
      );
  }

  Future<List<Entry>> _entryListExtraLoader() async
  {
    _loaderPage += 1;

    var pagedQuery = PagedQuery(
      currentPage: _loaderPage,
      limitPerPage: 10 
    );

    await _model.jwtService.tryRefreshToken();

    var items = await EntryApi.getNewest(pagedQuery: pagedQuery, jwtToken: _model.jwtService.jwtToken);

    if (items == null || items.isEmpty)
      return null;

    return items;
  }

  void _addPost() async
  {
    var result = await Navigator.push<Entry>(context, MaterialPageRoute(
        builder: (context) => AddPostPage()
      )
    );

    if (result == null)
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Something went wrong :/"),
      ));
      return;
    }

    _scaffoldKey.currentState.showSnackBar(SnackBar(
      content: Text("Post added!"),
      action: SnackBarAction(
        label: "Show",
        onPressed: () {
          Navigator.push(context, MaterialPageRoute(
            builder: (_) => EntryDetailPage(entry: result)
          ));
        },
      ),
    ));

    _entryListKey.currentState.reloadEntries();
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
