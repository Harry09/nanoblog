import 'package:flutter/material.dart';
import 'package:nanoblog/api/entry_api.dart';
import 'package:nanoblog/exceptions/api_exception.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/entry.dart';
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
          borderRadius: BorderRadius.all(Radius.circular(10))
        ),
        padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
        margin: EdgeInsets.symmetric(vertical: 5),
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
    return Align(
      alignment: Alignment.centerRight,
      child: IconButton(
        icon: Icon(Icons.more_vert),
        onPressed: () => _showMoreOptions(context),
        padding: EdgeInsets.all(0),
      ),
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
            _deletePost(context);
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

  
  Future _deletePost(BuildContext context) async
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
}