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

    var karma = json['value'] as int;

    if (karma >= 1)
    {
      karmaValue = KarmaValue.Plus;
    }
    else if (karma <= -1)
    {
      karmaValue = KarmaValue.Minus;
    }
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