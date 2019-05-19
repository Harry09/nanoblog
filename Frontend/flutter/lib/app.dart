import 'package:flutter/material.dart';
import 'package:nanoblog/screnns/home.dart';

class NanoblogApp extends StatefulWidget
{
  State<StatefulWidget> createState() => new NanoblogState();
}

class NanoblogState extends State<NanoblogApp>
{
  @override
  Widget build(BuildContext context)
  {
    return MaterialApp( 
      title: "Todo App",
      home: HomePage(),
      theme: ThemeData(
        primaryColor: Colors.white
      ),
    );
  }
}
