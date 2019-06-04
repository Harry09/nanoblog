import 'package:flutter/material.dart';
import 'package:tuple/tuple.dart';

class LoginPage extends StatefulWidget
{
  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage>
{
  var email = TextEditingController();
  var password = TextEditingController();

  final _scaffoldKey = GlobalKey<ScaffoldState>();

  @override
  void dispose()
  {
    email.dispose();
    password.dispose();
    super.dispose();
  }

  void login(BuildContext context)
  {
    if (email.text.isEmpty && password.text.isEmpty)
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Fill in all fields!"),
      ));
    }
    else
    {
      Navigator.pop(context, Tuple2<String, String>(email.text, password.text));
    }
  }

  @override
  Widget build(BuildContext context)
  {
    return Scaffold(
      key: _scaffoldKey,
      appBar: AppBar(
        title: Text("Login"),
      ),
      body: Container(
        padding: EdgeInsets.all(16),
        child: Column(
          children: <Widget>[
            TextField(
              controller: email,
              decoration: InputDecoration(
                hintText: "E-Mail"
              ),
            ),
            TextField(
              controller: password,
              decoration: InputDecoration(
                hintText: "Password"
              ),
              obscureText: true,
          ),
            SizedBox(
              width: double.infinity,
              child: RaisedButton(
                child: Text("Login"),
                onPressed: () => login(context),
              )
            )
          ],
        ),
      ),
    );
  }
}