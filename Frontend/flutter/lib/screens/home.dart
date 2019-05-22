import 'package:flutter/material.dart';

class HomePage extends StatefulWidget
{
  @override
  State<StatefulWidget> createState() => HomePageState();
}

class HomePageState extends State<HomePage>
{
  Widget _buildPostHeader()
  {
    return 
    Container(
      padding: EdgeInsets.symmetric(vertical: 10),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            padding: EdgeInsets.symmetric(horizontal: 10),
            child: Text(
              "H4RRY",
              style: TextStyle(
                fontWeight: FontWeight.bold
                )
              ),
          ),
          Text("21.05.2019")
        ],
      )
    );
  }

  Widget _buildPostBody()
  {
    return Flex(
      direction: Axis.vertical,
      children: [
        Text("Lorem ipsum dolor sit amet, consectetur adipisicing elit. Velit voluptate harum dolores distinctio enim aspernatur fugiat soluta minima rerum tempore.")
      ],
    );
  }

  Widget _buildPostFooter()
  {

  }

  Widget _buildPost()
  {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        _buildPostHeader(),
        _buildPostBody(),
        //_buildPostFooter()
      ],
    );
  }

  Widget _buildBody()
  {
    return Container(
      padding: EdgeInsets.all(20),
      child: _buildPost()
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Nanoblog"),
      ),
      body: _buildBody(),
    );
  }
}
