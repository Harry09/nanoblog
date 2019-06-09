
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';

typedef BoolCallback = Future<bool> Function();

class LoadingPage extends StatefulWidget
{
  final BoolCallback toProcess;
  final Function onSuccess;
  final Function onFail;

  final String message;

  const LoadingPage({Key key, @required this.toProcess, @required this.onSuccess, @required this.onFail, this.message}) : super(key: key);

  @override
  _LoadingPageState createState() => _LoadingPageState();
}

class _LoadingPageState extends State<LoadingPage> {

  @override
  void initState()
  {
    super.initState();

    SchedulerBinding.instance.addPostFrameCallback((_) {
      _doTask();
    });
  }

  Future _doTask() async 
  {
    if (await widget.toProcess())
    {
      widget.onSuccess();
    }
    else
    {
      widget.onFail();
    }
  }

  @override
  Widget build(BuildContext context) {
    
    List<Widget> columnItems = List<Widget>();

    if (this.widget.message != null && this.widget.message.isNotEmpty)
    {
      columnItems.add(Text(
        this.widget.message,
        style: TextStyle(
          fontSize: 32
        ),
      ));
    }

    columnItems.add(CircularProgressIndicator());

    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: columnItems,
        ),
      )
    );
  }
}