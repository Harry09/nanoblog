import 'package:flutter/material.dart';
import 'package:nanoblog/api/entry_api.dart';
import 'package:nanoblog/api/karma_api.dart';
import 'package:nanoblog/exceptions/api_exception.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/util/karma_value.dart';
import 'package:scoped_model/scoped_model.dart';

class EntryListItem extends StatefulWidget
{
  const EntryListItem({Key key, this.entry, this.onEntryDeleted, this.onTap}) : super(key: key);

  final Entry entry;

  final Function onEntryDeleted;
  final Function onTap;

  @override
  _EntryListItemState createState() => _EntryListItemState();
}

class _EntryListItemState extends State<EntryListItem> {

  AppStateModel _model;

  int _karmaCount;
  KarmaValue _userVote;

  @override
  void initState()
  {
    super.initState();

    _karmaCount = widget.entry.karmaCount;
    _userVote = widget.entry.userVote;
  }

  @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of(context, rebuildOnChange: true);

    return GestureDetector(
      onTap: widget.onTap,
      child: Container(
        decoration: BoxDecoration(
          color: Colors.white,
          border: Border.all(color: Colors.white),
        ),
        padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            _buildHeader(context),
            _buildBody(context),
            _buildFooter(context)
          ],
        )
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
                widget.entry.author.userName,
                style: TextStyle(
                  fontWeight: FontWeight.bold
                )
              ),
            ),
          ),
          Text(widget.entry.createTime)
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
            Text(widget.entry.text)
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
          width: 70,
          height: 28,
          child: FlatButton.icon(
            icon: Icon(Icons.comment, size: 24),
            label: Text(widget.entry.commentsCount.toString()),
            padding: EdgeInsets.all(2),
            onPressed: widget.onTap,
          )
        ),
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
            ],
          ),
        ),
        SizedBox(
          width: 28,
          height: 28,
          child: FlatButton(
            child: Icon(Icons.more_vert, size: 24,),
            padding: EdgeInsets.all(2),
            onPressed: () => _showMoreOptions(context),
          ),
        ),
      ],
    );
  }

  void _showMoreOptions(BuildContext context)
  {
    List<Widget> columnItems;

    if (_model.currentUser != null && _model.currentUser.id == widget.entry.author.id)
    {
      columnItems = [
        ListTile(
          leading: Icon(Icons.delete),
          title: Text("Delete entry"),
          onTap: () {
            Navigator.pop(context);
            _deleteEntry(context);
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

  Future _deleteEntry(BuildContext context) async
  {
    if (_model.jwtService.jwtToken == null)
      return;

    _model.jwtService.tryRefreshToken();

    try
    {
      if (await EntryApi.deleteEntry(widget.entry.id, _model.jwtService.jwtToken))
      {
        widget.onEntryDeleted();
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
      await KarmaApi.removeVote(entryId: widget.entry.id, jwtToken: _model.jwtService.jwtToken);
    }
    else
    {
      switch (value)
      {
        case KarmaValue.Plus:
          await KarmaApi.upVote(entryId: widget.entry.id, jwtToken: _model.jwtService.jwtToken);
          break;
        case KarmaValue.Minus:
          await KarmaApi.downVote(entryId: widget.entry.id, jwtToken: _model.jwtService.jwtToken);
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

    var karmaCount = await KarmaApi.countVotes(entryId: widget.entry.id);
    var userVote = await KarmaApi.getUserVote(userId: _model.currentUser.id, entryId: widget.entry.id);

    setState(() {
      _karmaCount = karmaCount;
      _userVote = userVote; 
    });
  }
}