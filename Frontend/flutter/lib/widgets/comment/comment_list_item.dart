
import 'package:flutter/material.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:scoped_model/scoped_model.dart';

class CommentListItem extends StatefulWidget
{
  const CommentListItem({Key key, this.comment, this.onCommentDeleted}) : super(key: key);

  final Comment comment;
  final Function onCommentDeleted;

  @override
  _CommentListItemState createState() => _CommentListItemState();
}

class _CommentListItemState extends State<CommentListItem>
{
  AppStateModel _model;

  @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of(context, rebuildOnChange: true);

    return Container(
      decoration: BoxDecoration(
        color: Colors.white
      ),
      padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
      margin: EdgeInsets.symmetric(vertical: 2),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          _buildHeader(context),
          _buildBody(context),
          _buildFooter(context)
        ],
      ),
    );
  }

  Widget _buildHeader(BuildContext context)
  {
    return Container(
      padding: EdgeInsets.symmetric(vertical: 10),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          GestureDetector(
            onTap: () {}, // TODO: show profile
            child: Container(
              padding: EdgeInsets.symmetric(horizontal: 5),
              child: Text(
                widget.comment.author.userName,
                style: TextStyle(
                  fontWeight: FontWeight.bold
                )
              ),
            ),
          ),
          Text(widget.comment.createTime)
        ],
      )
    );
  }

  Widget _buildBody(BuildContext context)
  {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 5),
      child: Align(
        alignment: Alignment.centerLeft,
        child: Flex(
          direction: Axis.vertical,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Text(widget.comment.text)
          ],
        )
      )
    );
  }

  Widget _buildFooter(BuildContext context)
  {
    return Align(
      alignment: Alignment.centerRight,
      child: IconButton(
        icon: Icon(Icons.more_vert),
        onPressed: () {}, // TODO: Show more options
        padding: EdgeInsets.all(0),
      ),
    );
  }
}