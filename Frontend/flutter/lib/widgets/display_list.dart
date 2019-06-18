import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

enum _ListStatus
{
  Empty,
  Loading,
  Loaded
}

typedef ListLoader<ItemType> = Future<List<ItemType>> Function();

class DisplayList<ItemType> extends StatefulWidget
{
  const DisplayList({Key key, this.loader, this.itemBuilder}) : super(key: key);

  final ListLoader<ItemType> loader;

  final IndexedWidgetBuilder itemBuilder;

  @override
  DisplayListState<ItemType> createState() => DisplayListState<ItemType>();
}

class DisplayListState<ItemType> extends State<DisplayList>
{
  List<ItemType> _items;

  _ListStatus _status = _ListStatus.Empty;

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
            child: Text(
              "Nothing here :(",
              style: TextStyle(
                fontSize: 32
              ),
              ),
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
        child: ListView.builder(
          padding: EdgeInsets.all(10),
          itemCount: _items.length,
          itemBuilder: widget.itemBuilder
        ), 
      );
    }

    return null;
  }

  ItemType getItem(int index) 
  {
    return _items.elementAt(index);
  }

  Future reloadItems() async
  {
    setState(() {
      _status = _ListStatus.Loading;
    });

    final entries = await widget.loader();

    setState(() {
      this._items = entries;

      if (this._items.isEmpty)
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
