import 'package:flutter/material.dart';

class AddPostPage extends StatefulWidget
{
  @override
  _AddPostPageState createState() => _AddPostPageState();
}

class _AddPostPageState extends State<AddPostPage>
{
  var messageController = TextEditingController();

  final _scaffoldKey = GlobalKey<ScaffoldState>();

  @override
  void dispose()
  {
    messageController.dispose();
    super.dispose();
  }

  void onSubmit(BuildContext context)
  {
    if (messageController.text.isEmpty)
    {
      _scaffoldKey.currentState.showSnackBar(SnackBar(
        content: Text("Enter a message!"),
      ));
    }
    // else
    // {
    //   Navigator.pop(context, messageController.text);
    // }
  }

  @override
  Widget build(BuildContext context)
  {
    return Scaffold(
      key: _scaffoldKey,
      appBar: AppBar(
        title: Text("Add post"),
        actions: <Widget>[
          IconButton(
            icon: Icon(Icons.arrow_forward),
            onPressed: () => onSubmit(context),
          )
        ],
      ),
      body: Container(
        padding: EdgeInsets.all(16),
        child: TextField(
          decoration: InputDecoration(
            hintText: "Insert your message"
          ),
          keyboardType: TextInputType.multiline,
          scrollPadding: EdgeInsets.all(20),
          maxLines: 999999,
          autofocus: true,
          controller: messageController,
        )
      ),
    );
  }
}