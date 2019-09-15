import 'package:nanoblog/model/author.dart';
import 'package:nanoblog/util/karma_value.dart';

class Comment
{
  String id;
  Author author;
  String text;
  String createTime;
  int karmaCount;
  KarmaValue userVote;

  Comment({this.id, this.author, this.text, this.createTime, this.karmaCount, this.userVote});
}