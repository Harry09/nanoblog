
import 'package:nanoblog/model/user.dart';

class Entry
{
  String id;
  User author;
  String text;
  String createTime;

  Entry({this.id, this.author, this.text, this.createTime});

  Entry.fromJson(Map<String, dynamic> json)
  {
    this.id = json['id'];
    this.author = User.fromJson(json['author']);
    this.text = json['text'];
    this.createTime = json['createTime'];
  }
}
