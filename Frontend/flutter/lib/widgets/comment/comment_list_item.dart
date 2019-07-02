
import 'package:flutter/material.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/util/karma_value.dart';
import 'package:scoped_model/scoped_model.dart';
import 'package:nanoblog/exceptions/api_exception.dart';

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

  int _karmaCount;
  KarmaValue _userVote;

  @override
  void initState()
  {
    super.initState();

    _karmaCount = widget.comment.karmaCount;
    _userVote = widget.comment.userVote;
  }

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
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: <Widget>[
        SizedBox(
          width: 80,
          height: 28,
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              SizedBox(
                width: 24,
                height: 24,
                child: FlatButton(
                  padding: EdgeInsets.all(0),
                  child: Icon(
                    Icons.remove,
                    color: _userVote == KarmaValue.Minus ? Colors.red : Colors.black
                  ),
                  onPressed: () => _changeVote(KarmaValue.Minus),
                ),
              ),
              Text(
                _karmaCount.toString(),
                style: TextStyle(
                  fontSize: 16
                ),
              ),
              SizedBox(
                width: 24,
                height: 24,
                child: FlatButton(
                  padding: EdgeInsets.all(0),
                  child: Icon(
                    Icons.add, 
                    color: _userVote == KarmaValue.Plus ? Colors.green[300] : Colors.black
                  ),
                  onPressed: () => _changeVote(KarmaValue.Plus),
                ),
              ),
            ]
          ),
        ),
        SizedBox(
          width: 28,
          height: 28,
          child: FlatButton(
            child: Icon(Icons.more_vert, size: 24,),
            padding: EdgeInsets.all(2),
            onPressed: () => _showMoreOptions(context)
          ),
        )
      ]
    );
  }

  void _showMoreOptions(BuildContext context)
  {
    List<Widget> columnItems;

    if (_model.currentUser != null && _model.currentUser.id == widget.comment.author.id)
    {
      columnItems = [
        ListTile(
          leading: Icon(Icons.delete),
          title: Text("Delete comment"),
          onTap: () {
            Navigator.pop(context);
            _deleteComment(context);
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
      context: context,
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

  Future _deleteComment(BuildContext context) async
  {
    if (_model.jwtService.jwtToken == null)
      return;

    _model.jwtService.tryRefreshToken();

    try
    {
      if (await _model.commentRepository.deleteComment(widget.comment.id, _model.jwtService.jwtToken))
      {
        widget.onCommentDeleted();
      }
      else
      {
        Scaffold.of(context).showSnackBar(SnackBar(
          content: Text("Cannot remove post!"),
        ));
      }
    }
    on ApiException catch (ex)
    {
      Scaffold.of(context).showSnackBar(SnackBar(
          content: Text(ex.toString()),
        ));
    }
  }

  Future _changeVote(KarmaValue value) async
  {
    if (_userVote == value)
    {
      await _model.karmaRepository.removeVote(commentId: widget.comment.id, jwtToken: _model.jwtService.jwtToken);
    }
    else
    {
      switch (value)
      {
        case KarmaValue.Plus:
          await _model.karmaRepository.upVote(commentId: widget.comment.id, jwtToken: _model.jwtService.jwtToken);
          break;
        case KarmaValue.Minus:
          await _model.karmaRepository.downVote(commentId: widget.comment.id, jwtToken: _model.jwtService.jwtToken);
          break;
        case KarmaValue.None:
          break;
      }
    }

    await _refreshKarma();
  }

  Future _refreshKarma() async
  {
    await _model.jwtService.tryRefreshToken();

    var karmaCount = await _model.karmaRepository.countVotes(commentId: widget.comment.id);
    var userVote = await _model.karmaRepository.getUserVote(userId: _model.currentUser.id, commentId: widget.comment.id);

    setState(() {
      _karmaCount = karmaCount;
      _userVote = userVote; 
    });
  }
} 
