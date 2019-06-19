
import 'package:flutter/material.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/widgets/comment/comment_list_item.dart';
import 'package:nanoblog/widgets/entry/entry_list_item.dart';
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

  @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of(context, rebuildOnChange: true);

    return Scaffold(
      backgroundColor: Colors.grey[200],
      appBar: AppBar(
        title: Text("Entry Detail"),
      ),
      body: _buildBody()
    );
  }

  Widget _buildBody()
  {
    return FutureBuilder<List<Widget>>(
      initialData: <Widget>[
        EntryListItem(
          entry: widget.entry,
          onEntryDeleted: () {},
        )
      ],
      future: _loadData(),
      builder: (ctx, AsyncSnapshot<List<Widget>> snapshot) {
        switch(snapshot.connectionState)
        {
          case ConnectionState.none:
            return Center(
              child: Padding(
                padding: EdgeInsets.all(16),
                child: Text(
                  "Nothing here :(",
                  style: TextStyle(
                    fontSize: 32
                  ),
                ),
              ),
            );
          case ConnectionState.waiting:
            return Center(
              child: Padding(
                padding: EdgeInsets.all(16),
                child: CircularProgressIndicator()
              ),
            );
          case ConnectionState.active:
          case ConnectionState.done:
            return ListView(
              children: snapshot.data,
            );
        }
      },
    );
  }

  Future<List<Widget>> _loadData() async
  {
    var widgets = <Widget>[
      EntryListItem(
        entry: widget.entry,
        onEntryDeleted: () {},
      ),
      Container(
        padding: EdgeInsets.all(10),
        child: Text(
          "Comments",
          style: TextStyle(
            fontSize: 16
          ),
        )
      )
    ];

    var comments = await _model.commentRepository.getComments(widget.entry.id);

    for (var comment in comments)
    {
      widgets.add(CommentListItem(
        comment: comment
      ));
    }

    return widgets;
  }
}