import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:flutter/widgets.dart';
import 'package:nanoblog/widgets/comment/comment_list_item.dart';
import 'package:nanoblog/widgets/display_list.dart';
import 'package:nanoblog/model/comment.dart';


class CommentList extends StatefulWidget
{
  const CommentList({Key key, this.loader, this.refreshIndicator = false}) : super(key: key);

  final ListLoader<Comment> loader;

  final bool refreshIndicator;

  @override
  _CommentListState createState() => _CommentListState();
}

class _CommentListState extends State<CommentList>
{
  var _displayListKey = GlobalKey<DisplayListState<Comment>>();

  @override
  void initState()
  {
    super.initState();

    SchedulerBinding.instance.addPostFrameCallback((_) {
      reloadComments();
    });
  }

  @override
  Widget build(BuildContext context)
  {
    var list = DisplayList<Comment>(
      key: _displayListKey,
      loader: widget.loader,
      itemBuilder: (ctxt, index) => CommentListItem(
        comment: _displayListKey.currentState.getItem(index)
      )
    );

    Widget body;

    if (widget.refreshIndicator)
    {
      body = RefreshIndicator(
        onRefresh: reloadComments,
        child: list
      );
    }
    else
    {
      body = list;
    }

    return Expanded(
      child: Container(
        color: Colors.grey[200],
        child: body
      ),
    );
  }

  Future reloadComments() async
  {
    await _displayListKey.currentState.reloadItems();
  }
}