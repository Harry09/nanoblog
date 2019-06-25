import 'package:flutter/material.dart';
import 'package:nanoblog/screens/home.dart';
import 'package:nanoblog/screens/login.dart';
import 'package:nanoblog/screens/startup.dart';

class NanoblogApp extends StatelessWidget
{
  @override
  Widget build(BuildContext context)
  {
    return MaterialApp( 
      title: "Todo App",
      routes: <String, WidgetBuilder> {
        "/home": (_) => HomePage(),
        "/login": (_) => LoginPage(),
      },
      home: StartupPage(),
      theme: ThemeData(
        primaryColor: Colors.white,
        accentColor: Colors.teal
      ),
    );
  }
}
