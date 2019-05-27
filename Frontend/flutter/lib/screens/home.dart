import 'package:flutter/material.dart';
import 'package:nanoblog/api/entry_api.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/user.dart';
import 'package:scoped_model/scoped_model.dart';

class HomePage extends StatefulWidget
{
  @override
  State<StatefulWidget> createState() => HomePageState();
}

class HomePageState extends State<HomePage>
{
  List<Entry> entries;

  HomePageState()
  {
    entries = List<Entry>();
    mockData();
  }

  Widget _buildPostHeader(Entry entry)
  {
    return 
    Container(
      padding: EdgeInsets.symmetric(vertical: 10),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            padding: EdgeInsets.symmetric(horizontal: 5),
            child: Text(
              entry.author.userName,
              style: TextStyle(
                fontWeight: FontWeight.bold
                )
              ),
          ),
          Text(entry.createTime)
        ],
      )
    );
  }

  Widget _buildPostBody(Entry entry)
  {
    return Align(
    alignment: Alignment.centerLeft,
    child: Flex(
        direction: Axis.vertical,
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Text(entry.text)
        ],
      )
    );
  }

  Widget _buildPostFooter(Entry entry)
  {

  }

  Widget _buildPost(Entry entry)
  {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        border: Border.all(color: Colors.white),
        borderRadius: BorderRadius.all(Radius.circular(10))
      ),
      padding: EdgeInsets.fromLTRB(10, 5, 10, 10),
      margin: EdgeInsets.symmetric(vertical: 5),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          _buildPostHeader(entry),
          _buildPostBody(entry),
          //_buildPostFooter()
        ],
      )
    );
  }

  Widget _buildBody()
  {
    if (entries == null)
    {
      return Container(
        padding: EdgeInsets.all(10),
        child: Text("Nic tutaj nie ma :(")
      );
    }    

    return Container(
      color: Colors.grey[200],
      child: ListView.builder(
        padding: EdgeInsets.all(10),
        itemCount: entries.length,
        itemBuilder: (BuildContext ctxt, int index) => _buildPost(entries[index])
      )
    );
  }

  Future refreshData() async
  {
    var entries_ = await EntryApi.getEntries();
    setState(() {
      entries = entries_;
    });

  }

  void mockData()
  {
    var defaultUser = User(
      userName: "Harry"
    );

    entries.add(Entry(
      author: defaultUser,
      createTime: "10 hours ago",
      text: "Sint commodo proident pariatur in qui ea non. Anim aute culpa duis non sunt incididunt laborum nisi tempor."
    ));

    entries.add(Entry(
      author: defaultUser,
      createTime: "5 hours ago",
      text: "zażółć gęślą jaźń"
    ));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Nanoblog"),
        actions: <Widget>[
          IconButton(
            icon: Icon(Icons.refresh),
            onPressed: refreshData,
          )
        ],
      ),
      body: _buildBody(),
    );
  }
}
