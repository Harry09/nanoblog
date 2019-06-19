
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/screens/entry_detail.dart';
import 'package:nanoblog/widgets/entry/entry_list_item.dart';

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
  @override
  void initState()
  {
    super.initState();

    SchedulerBinding.instance.addPostFrameCallback((_) {
    });
  }

  @override
  Widget build(BuildContext context)
  {
    return Container(
      color: Colors.grey[200],
      child: RefreshIndicator(
        onRefresh: reloadEntries,
        child: FutureBuilder<List<Entry>>(
          future: widget.loader(),
          builder: (ctx, AsyncSnapshot snapshot) {
            switch (snapshot.connectionState)
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
                return _buildListView(ctx, snapshot);
            }
          },
        )
      )
    );
  }

  Widget _buildListView(BuildContext ctx, AsyncSnapshot<List<Entry>> snapshot)
  {
    return ListView.builder(
      itemCount: snapshot.data.length,
      itemBuilder: (ctx, i) => EntryListItem(
        entry: snapshot.data[i],
        onEntryDeleted: _onEntryDeleted,
        onTap: () => _onEntryTap(snapshot.data[i]),
      ),
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
    // force rebuild
    setState(() {});
  }
}
