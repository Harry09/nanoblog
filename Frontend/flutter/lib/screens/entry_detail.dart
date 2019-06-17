
import 'package:flutter/material.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/widgets/entry/entry_list_item.dart';

class EntryDetailPage extends StatefulWidget
{
  const EntryDetailPage({Key key, this.entry}) : super(key: key);

  final Entry entry;

  @override
  _EntryDetailPageState createState() => _EntryDetailPageState();
}

class _EntryDetailPageState extends State<EntryDetailPage>
{
  @override
  Widget build(BuildContext context)
  {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      appBar: AppBar(
        title: Text("Entry Detail"),
      ),
      body: EntryListItem(
        entry: widget.entry,
      ),
    );
  }

}