
import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/util/karma_value.dart';

class Entry
{
  String id;
  User author;
  String text;
  String createTime;
  int commentsCount;
  int karmaCount;
  KarmaValue userVote;

  Entry({this.id, this.author, this.text, this.createTime, this.commentsCount, this.karmaCount, this.userVote});
}
