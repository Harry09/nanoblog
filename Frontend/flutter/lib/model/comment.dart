import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/user.dart';

class Comment
{
  String id;
  User author;
  Entry parent;
  String text;
  String createTime;

  Comment({this.id, this.author, this.parent, this.text, this.createTime});
}