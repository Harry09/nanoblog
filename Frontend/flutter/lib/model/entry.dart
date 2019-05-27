
import 'package:nanoblog/model/user.dart';

class Entry
{
  String id;
  User author;
  String text;
  String createTime;

  Entry({String id, User author, String text, String createTime})
  {
    this.id = id;
    this.author = author;
    this.text = text;
    this.createTime = createTime;
  }

  Entry.fromJson(Map json)
  {
    this.id = json['id'];
    this.author = User.fromJson(json['author']);
    this.text = json['text'];
    this.createTime = json['createTime'];
  }
}
