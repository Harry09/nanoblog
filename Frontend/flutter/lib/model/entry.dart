
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/user.dart';

class Entry
{
  String id;
  User author;
  String text;
  String createTime;

  List<Comment> comments;

  Entry({this.id, this.author, this.text, this.createTime, this.comments});
}
