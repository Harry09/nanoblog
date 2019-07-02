import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/util/karma_value.dart';

class EntryResponse
{
  String id;
  String authorId;
  String text;
  String createTime;
  int commentsCount;
  int karmaCount;
  KarmaValue userVote;

  EntryResponse({this.id, this.authorId, this.text, this.createTime, this.commentsCount});

  EntryResponse.fromJson(Map<String, dynamic> json)
  {
    id = json['id'];
    authorId = json['authorId'];
    text = json['text'];
    createTime = json['createTime'];
    commentsCount = json['commentsCount'];
    karmaCount = json['karmaCount'];
    userVote = getKarmaValueFromInt(json['userVote'] as int);
  }

  Entry toEntry(User author)
  {
    return Entry(
      id: id,
      author: author,
      text: text,
      createTime: createTime,
      commentsCount: commentsCount,
      karmaCount: karmaCount,
      userVote: userVote
    );
  }
}
