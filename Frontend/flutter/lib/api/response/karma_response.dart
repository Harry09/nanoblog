import 'package:nanoblog/util/karma_value.dart';

class KarmaResponse
{
  String id;

  String authorId;

  KarmaValue karmaValue;

  KarmaResponse.fromJson(Map<String, dynamic> json)
  {
    id = json['id'];
    authorId = json['authorId'];

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
}