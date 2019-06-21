import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/user.dart';

class Comment
{
  String id;
  User author;
  String text;
  String createTime;

  Comment({this.id, this.author, this.text, this.createTime});
}