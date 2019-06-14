
import 'package:nanoblog/model/user.dart';

class Entry
{
  String id;
  User author;
  String text;
  String createTime;

  Entry({this.id, this.author, this.text, this.createTime});
}
