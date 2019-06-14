
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/entry_detail.dart';
import 'package:nanoblog/widgets/entry_list_item.dart';

typedef EntryListLoader = Future<List<Entry>> Function();

enum _ListStatus
{
  Empty,
  Loading,
  Loaded
}

class EntryList extends StatefulWidget
{
  const EntryList({Key key, this.loader}) : super(key: key);

  final EntryListLoader loader;

  @override
  EntryListState createState() => EntryListState();
}

class EntryListState extends State<EntryList>
{
  List<Entry> _entries;

  _ListStatus _status = _ListStatus.Empty;

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
      child: _buildWidget(),
    );
  }

  Widget _buildWidget()
  {
    switch (_status)
    {
      case _ListStatus.Empty:
        return Center(
          child: Padding(
            padding: EdgeInsets.all(16),
            child: Text("Nothing here :("),
          ),
        );
      case _ListStatus.Loading:
        return Center(
          child: Padding(
            padding: EdgeInsets.all(16),
            child: CircularProgressIndicator()
            ),
        );
      case _ListStatus.Loaded:
        return Center(
        child: RefreshIndicator(
          onRefresh: reloadEntries,
          child: ListView.builder(
            padding: EdgeInsets.all(10),
            itemCount: _entries.length,
            itemBuilder: (BuildContext ctxt, int index) {
              return EntryListItem(
                entry: _entries[index],
                onEntryDeleted: _onEntryDeleted,
                onTap: () => _onEntryTap(_entries[index]),
                );
            }
          ),
        ), 
      );
    }

    return null;
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
    setState(() {
      _status = _ListStatus.Loading;
    });

    final entries = await widget.loader();
    setState(() {
      this._entries = entries;

      if (this._entries.isEmpty)
      {
        _status = _ListStatus.Empty;
      }
      else
      {
        _status = _ListStatus.Loaded;
      }
    });
  }
}
