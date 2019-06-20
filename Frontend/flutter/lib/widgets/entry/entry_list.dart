
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/entry_detail.dart';
import 'package:nanoblog/widgets/entry/entry_list_item.dart';
import 'package:nanoblog/widgets/future_list_view.dart';

typedef EntryListLoader = Future<List<Entry>> Function();

class EntryList extends StatefulWidget
{
  const EntryList({Key key, this.loader}) : super(key: key);

  final EntryListLoader loader;

  @override
  EntryListState createState() => EntryListState();
}

class EntryListState extends State<EntryList>
{
  var _listViewKey = GlobalKey<FutureListViewState<Entry>>();

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
    return Container(
      color: Colors.grey[200],
      child: RefreshIndicator(
        onRefresh: reloadEntries,
        child: FutureListView<Entry>(
          key: _listViewKey,
          loader: widget.loader,
          loadedBuilder: (ctx, entry) => Container(
            margin: EdgeInsets.symmetric(vertical: 3),
            child: EntryListItem(
              entry: entry,
              onEntryDeleted: _onEntryDeleted,
              onTap: () => _onEntryTap(entry)
            )
          )
        )
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
    await _listViewKey.currentState.reloadItems();
  }
}
