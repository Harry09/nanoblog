import 'package:nanoblog/api/response/user_response.dart';
import 'package:nanoblog/model/karma.dart';
import 'package:nanoblog/util/karma_value.dart';

class KarmaResponse
{
  String id;
  UserResponse author;
  KarmaValue karmaValue;

  KarmaResponse.fromJson(Map<String, dynamic> json)
  {
    id = json['id'];
    author = json['author'];
    karmaValue = getKarmaValueFromInt(json['value'] as int);
  }

  Karma toKarma()
  {
    return Karma(
      id: id,
      author: author.toUser(),
      karmaValue: karmaValue
    );
  }
}