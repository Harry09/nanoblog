import 'package:flutter/material.dart';

typedef FutureListViewLoader<T> = Future<List<T>> Function();
typedef FutureListViewBuilider = Widget Function(BuildContext);
typedef LoadedListViewBuilider<T> = Widget Function(BuildContext, T);

enum _ListStatus
{
  Empty,
  Waiting,
  Loaded
}

class FutureListView<T> extends StatefulWidget
{
  const FutureListView({Key key, @required this.loader, this.emptyBuilder, this.waitingBuilder, @required this.loadedBuilder, this.padding}) : super(key: key);

  final EdgeInsetsGeometry padding;

  final FutureListViewLoader<T> loader;

  final FutureListViewBuilider emptyBuilder;
  final FutureListViewBuilider waitingBuilder;
  final LoadedListViewBuilider<T> loadedBuilder;

  @override
  FutureListViewState<T> createState() => FutureListViewState<T>();
}

class FutureListViewState<T> extends State<FutureListView<T>>
{
  List<T> _items;

  _ListStatus _status = _ListStatus.Empty;

  @override
  Widget build(BuildContext context)
  {
    return _buildWidget(context);
  }

  Widget _buildWidget(BuildContext context)
  {
    Widget _emptyBulider;
    Widget _waitingBuilder;

    if (widget.emptyBuilder != null)
    {
      _emptyBulider = widget.emptyBuilder(context);
    }
    else
    {
      _emptyBulider = Center(
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
    }

    if (widget.waitingBuilder != null)
    {
      _waitingBuilder = widget.waitingBuilder(context);
    }
    else
    {
      _waitingBuilder = Center(
        child: Padding(
          padding: EdgeInsets.all(16),
          child: CircularProgressIndicator()
          ),
      );
    }

    switch (_status)
    {
      case _ListStatus.Empty:
        return _emptyBulider;
      case _ListStatus.Waiting:
        return _waitingBuilder;
      case _ListStatus.Loaded:
        return Center(
        child: ListView.builder(
          padding: widget.padding,
          itemCount: _items.length,
          itemBuilder: (ctx, i) => widget.loadedBuilder(ctx, _items[i])
        ), 
      );
    }

    return null;
  }

  Future reloadItems() async
  {
    setState(() {
      _status = _ListStatus.Waiting;
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
