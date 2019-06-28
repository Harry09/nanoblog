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
  const FutureListView(
    {Key key, 
    @required this.loader, 
    this.emptyBuilder, 
    this.waitingBuilder, 
    @required this.loadedBuilder, 
    this.padding, 
    this.extraLoader, 
    this.extraLoadingMessageBuilder, 
    this.extraLoaderMinimum = 0.8}) : super(key: key);

  final EdgeInsetsGeometry padding;

  final FutureListViewLoader<T> loader;
  final FutureListViewLoader<T> extraLoader;

  final FutureListViewBuilider emptyBuilder;
  final FutureListViewBuilider waitingBuilder;
  final LoadedListViewBuilider<T> loadedBuilder;
  final FutureListViewBuilider extraLoadingMessageBuilder;

  final double extraLoaderMinimum;

  @override
  FutureListViewState<T> createState() => FutureListViewState<T>();
}

class FutureListViewState<T> extends State<FutureListView<T>>
{
  List<T> _items = List<T>();

  _ListStatus _status = _ListStatus.Empty;

  // used to load more items
  bool _extraLoadingAvailable = true;

  // used to display loading message
  bool _extraLoading = false;

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
    switch (_status)
    {
      case _ListStatus.Empty:
      {
        Widget _emptyBulider;

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

        return _emptyBulider;
      }
      case _ListStatus.Waiting:
      {
        Widget _waitingBuilder;

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

        return _waitingBuilder;
      }
      case _ListStatus.Loaded:
      {
        var renderItems = List<Widget>();

        for (var item in _items)
        {
          renderItems.add(widget.loadedBuilder(context, item));
        }

        if (_extraLoading)
        {
          if (widget.extraLoadingMessageBuilder == null)
          {
            renderItems.add(Padding(
              padding: EdgeInsets.all(16),
              child: Center(
                child: Text("Loading more...")
              ),
            ));
          }
          else
          {
            renderItems.add(widget.extraLoadingMessageBuilder(context));
          }
        }

        return Center(
          child: ListView(
            controller: _scrollController,
            padding: widget.padding,
            children: renderItems
          )
        );
      }
    }

    return null;
  }

  void _scrollUpdate() async
  {
    if (_extraLoadingAvailable == false)
      return;

    var position = _scrollController.position;

    var progress = position.pixels / position.maxScrollExtent;

    if (progress > widget.extraLoaderMinimum)
    {
      _extraLoadingAvailable = false;

      setState(() {
        _extraLoading = true; 
      });

      var newItems = await widget.extraLoader();

      setState(() {
        _extraLoading = false; 
      });

      if (newItems != null)
      {
        _extraLoadingAvailable = true;

        setState(() {
          _items.addAll(newItems);
        });
      }
      else
      {
        // if no more data will be added
        _extraLoadingAvailable = false;
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

      _items.clear();
      _items.addAll(items);

      _extraLoadingAvailable = true;
      _extraLoading = false;

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
