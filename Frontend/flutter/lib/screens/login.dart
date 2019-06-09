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
  final _formKey = GlobalKey<FormState>();

  @override
  void dispose()
  {
    email.dispose();
    password.dispose();
    super.dispose();
  }

  void login(BuildContext context)
  {
    
    if (_formKey.currentState.validate())
    {
      Navigator.pop(context, Tuple2<String, String>(email.text, password.text));

      Navigator.
    }
  }

  String _validEmail(String value)
  {
    Pattern pattern =
        r'^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$';
    RegExp regex = new RegExp(pattern);
    if (!regex.hasMatch(value))
      return 'Enter Valid Email';
    else
      return null;
  }

  String _validPassword(String value)
  {
    if (value.isEmpty)
      return "Enter password";
    else
      return null;
  }

  @override
  Widget build(BuildContext context)
  {
    return Scaffold(
      key: _scaffoldKey,
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Container(
            alignment: Alignment.center,
            padding: EdgeInsets.all(20),
            child: Text(
              "Nanoblog",
              style: TextStyle(
                fontSize: 42
              ),
            )
          ),
          Form(
            key: _formKey,
            child:Theme(
              data: ThemeData(
                primarySwatch: Colors.teal,
                inputDecorationTheme: InputDecorationTheme(
                  labelStyle: TextStyle(
                    color: Colors.teal,
                  ),
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.all(Radius.circular(10)),
                    borderSide: BorderSide(
                      color: Colors.teal,
                      width: 2
                    )
                  ),
                )
              ), 
              child: Container(
                padding: EdgeInsets.symmetric(horizontal: 40),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: <Widget>[
                    TextFormField(
                      validator: _validEmail,
                      controller: email,
                      decoration: InputDecoration(
                        labelText: "Enter Email",
                      ),
                      keyboardType: TextInputType.emailAddress,
                    ),
                    Padding(
                      padding: EdgeInsets.only(top: 20),
                      child: TextFormField(
                        validator: _validPassword,
                        controller: password,
                        decoration: InputDecoration(
                          labelText: "Enter Password",
                        ),
                        keyboardType: TextInputType.text,
                        obscureText: true,
                      ),
                    ),
                    Padding(
                      padding: EdgeInsets.symmetric(vertical: 20),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: <Widget>[
                          FlatButton(
                            child: Text("Register"),
                            onPressed: () => login(context),
                            textColor: Colors.teal,
                          ),
                          MaterialButton(
                            color: Colors.teal,
                            textColor: Colors.white,
                            onPressed: () => login(context),
                            child: Text("Login"),
                          ),
                        ],
                      ),
                    )
                  ],
                ),
              ),
            )
          )
        ],
      )
    );
  }
}