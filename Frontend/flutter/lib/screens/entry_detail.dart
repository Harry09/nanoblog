
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
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

  var _listViewKey = GlobalKey<FutureListViewState>();

  @override
  void initState()
  {
    super.initState();

    SchedulerBinding.instance.addPostFrameCallback((_) {
      _listViewKey.currentState.reloadItems();
    });
  }

  @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of(context, rebuildOnChange: true);

    return Scaffold(
      backgroundColor: Colors.grey[200],
      appBar: AppBar(
        title: Text("Entry Detail"),
      ),
      body: RefreshIndicator(
        onRefresh: () => _listViewKey.currentState.reloadItems(),
        child: _buildBody()
      )
    );
  }

  Widget _buildBody()
  {
    return FutureListView<Widget>(
      key: _listViewKey,
      loader: _loadData,
      loadedBuilder: (ctx, w) => w,
    );
  }

  Future<List<Widget>> _loadData() async
  {
    var widgets = <Widget>[
      EntryListItem(
        entry: widget.entry,
        onEntryDeleted: () {},
      ),
    ];

    var comments = await _model.commentRepository.getComments(widget.entry.id);

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

  void _onCommentDeleted() async
  {
    _listViewKey.currentState.reloadItems();
  }
}