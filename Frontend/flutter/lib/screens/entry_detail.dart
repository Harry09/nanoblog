
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/add_comment.dart';
import 'package:nanoblog/widgets/comment/comment_list_item.dart';
import 'package:nanoblog/widgets/entry/entry_list_item.dart';
import 'package:nanoblog/widgets/future_list_view.dart';
import 'package:scoped_model/scoped_model.dart';

class EntryDetailPage extends StatefulWidget
{
  const EntryDetailPage({Key key, this.entry}) : super(key: key);

  final Entry entry;

  @override
  _EntryDetailPageState createState() => _EntryDetailPageState();
}

class _EntryDetailPageState extends State<EntryDetailPage>
{
  AppStateModel _model;

  var _commentsListKey = GlobalKey<FutureListViewState>();
  var _scaffoldKey = GlobalKey<ScaffoldState>();

  Entry _entry;

  int _loaderPage = 0;

  @override
  void initState()
  {
    super.initState();

    setState(() {
      _entry = widget.entry;
    });

    SchedulerBinding.instance.addPostFrameCallback((_) {
      _commentsListKey.currentState.reloadItems();
    });
  }

  @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of(context, rebuildOnChange: true);

    return Scaffold(
      key: _scaffoldKey,
      backgroundColor: Colors.grey[200],
      appBar: AppBar(
        title: Text("Entry Detail"),
      ),
      body: RefreshIndicator(
        onRefresh: _onRefresh,
        child: Padding(
          padding: EdgeInsets.only(bottom: 45),
          child: _buildBody()
        )
      ),
      bottomSheet: Container(
        padding: EdgeInsets.symmetric(horizontal: 10),
        child: TextField(
          decoration: InputDecoration(
            hintText: "Add comment"
          ),
          onTap: _addComment,
        ),
      ),
    );
  }

  Widget _buildBody()
  {
    if (_entry == null)
    {
      return Center(
        child: Text(
          "Entry not found!",
          style: TextStyle(
            fontSize: 32
          )),
      );
    }

    return FutureListView<Widget>(
      key: _commentsListKey,
      loader: _loadData,
      loadedBuilder: (ctx, w) => w,
      extraLoader: _extraLoader,
    );
  }

  Future<List<Widget>> _loadData() async
  {
    if (_entry == null)
      return null;

    _loaderPage = 0;

    var widgets = <Widget>[
      EntryListItem(
        entry: _entry,
        onEntryDeleted: _onRefresh
      ),
    ];

    var comments = await _model.commentRepository.getComments(
      _entry.id, 
      pagedQuery: PagedQuery(
        currentPage: 0,
        limitPerPage: 10
      )
    );

    if (comments.isEmpty)
    {
      widgets.add(Container(
        padding: EdgeInsets.all(16),
        child: Center(
          child: Text(
            "No comments",
            style: TextStyle(
              fontSize: 16
            )
          ),
        ),
      ));
    }
    else
    {
      widgets.add(Container(
        padding: EdgeInsets.all(10),
        child: Text(
          "Comments",
          style: TextStyle(
            fontSize: 16
          ),
        )
      ));
    }

    for (var comment in comments)
    {
      widgets.add(CommentListItem(
        comment: comment,
        onCommentDeleted: _onCommentDeleted,
      ));
    }

    return widgets;
  }

  Future<List<Widget>> _extraLoader() async
  {
    _loaderPage++;

    print("Loading more....");

    var items = await _model.commentRepository.getComments(
      widget.entry.id, 
      pagedQuery: PagedQuery(
        currentPage: _loaderPage,
        limitPerPage: 10
      )
    );

    if (items == null || items.isEmpty)
      return null;

    var widgets = List<Widget>();
    

    for (var item in items)
    {
      widgets.add(CommentListItem(
        comment: item,
        onCommentDeleted: _onCommentDeleted,
      ));
    }

    return widgets;
  }

  Future<void> _onRefresh() async
  {
    var entry = await _model.entryRepository.getEntry(_entry.id);

    setState(() {
     _entry = entry; 
    });

    await _commentsListKey.currentState.reloadItems();
  }

  void _onCommentDeleted() async
  {
    await _onRefresh();
  }

  void _addComment() async
  {
    var result = await Navigator.push(context, MaterialPageRoute(
      builder: (ctx) => AddCommentPage(entry: _entry)
    ));

    if (result == null)
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Something went wrong :/"),
      ));
      return;
    }

    await _onRefresh();
  }
}