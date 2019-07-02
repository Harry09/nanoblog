import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/util/karma_value.dart';

class Karma
{
  String id;
  User author;
  KarmaValue karmaValue;

  Karma({this.id, this.author, this.karmaValue});
}