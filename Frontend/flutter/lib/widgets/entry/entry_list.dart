
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/entry_detail.dart';
import 'package:nanoblog/widgets/display_list.dart';
import 'package:nanoblog/widgets/entry/entry_list_item.dart';

class EntryList extends StatefulWidget
{
  const EntryList({Key key, this.loader}) : super(key: key);

  final ListLoader<Entry> loader;

  @override
  EntryListState createState() => EntryListState();
}

class EntryListState extends State<EntryList>
{
  var _displayListKey = GlobalKey<DisplayListState<Entry>>();

  @override
  void initState()
  {
    super.initState();

    SchedulerBinding.instance.addPostFrameCallback((_) {
      reloadEntries();
    });
  }

  @override
  Widget build(BuildContext context)
  {
    return Container(
      color: Colors.grey[200],
      child: RefreshIndicator(
        onRefresh: reloadEntries,
        child: DisplayList<Entry>(
          key: _displayListKey,
          loader: widget.loader,
          itemBuilder: (ctxt, index) => 
            EntryListItem(
              entry: _displayListKey.currentState.getItem(index),
              onEntryDeleted: _onEntryDeleted,
              onTap: () => _onEntryTap(_displayListKey.currentState.getItem(index)),
            )
        ),
      )
    );
  }

  void _onEntryDeleted() async
  {
    await reloadEntries();
  }

  void _onEntryTap(Entry entry) async
  {
    Navigator.push(context, MaterialPageRoute(
      builder: (_) => EntryDetailPage(entry: entry)
    ));
  }

  Future reloadEntries() async
  {
    await _displayListKey.currentState.reloadItems();
  }
}
