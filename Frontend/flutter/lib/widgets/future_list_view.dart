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
  const FutureListView({Key key, @required this.loader, this.emptyBuilder, this.waitingBuilder, @required this.loadedBuilder, this.padding, this.extraLoader}) : super(key: key);

  final EdgeInsetsGeometry padding;

  final FutureListViewLoader<T> loader;
  final FutureListViewLoader<T> extraLoader;

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

  var _scrollController = ScrollController();

  @override
  void initState()
  {
    super.initState();

    _scrollController.addListener(_scrollUpdate);
  }

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
          controller: _scrollController,
          padding: widget.padding,
          itemCount: _items.length,
          itemBuilder: (ctx, i) => widget.loadedBuilder(ctx, _items[i])
        ), 
      );
    }

    return null;
  }

  void _scrollUpdate() async
  {
    var position = _scrollController.position;

    var progress = position.pixels / position.maxScrollExtent;

    if (progress > 0.8)
    {
      print("Loading more...");

      var newItems = await widget.extraLoader();

      if (newItems != null)
      {
        _items.addAll(newItems);
      }
    }
  }

  Future reloadItems() async
  {
    setState(() {
      _status = _ListStatus.Waiting;
    });

    final items = await widget.loader();

    setState(() {
      _items = items;

      if (_items == null || _items.isEmpty)
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
