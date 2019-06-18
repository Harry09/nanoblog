
import 'package:flutter/material.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/widgets/comment/comment_list.dart';
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
      body: Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          EntryListItem(
            entry: widget.entry,
          ),
          CommentList(
            loader: () => _model.commentRepository.getComments(widget.entry.id),
            refreshIndicator: false,
          )
        ]
      ),
    );
  }
}