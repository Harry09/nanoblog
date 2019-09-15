import 'package:nanoblog/api/response/author_response.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/util/karma_value.dart';

class EntryResponse
{
  String id;
  AuthorResponse author;
  String text;
  String createTime;
  int commentsCount;
  int karmaCount;
  KarmaValue userVote;

  EntryResponse({this.id, this.author, this.text, this.createTime, this.commentsCount});

  EntryResponse.fromJson(Map<String, dynamic> json)
  {
    id = json['id'];
    author = AuthorResponse.fromJson(json['author']);
    text = json['text'];
    createTime = json['createTime'];
    commentsCount = json['commentsCount'];
    karmaCount = json['karmaCount'];
    userVote = getKarmaValueFromInt(json['userVote'] as int);
  }

  Entry toEntry()
  {
    return Entry(
      id: id,
      author: author.toAuthor(),
      text: text,
      createTime: createTime,
      commentsCount: commentsCount,
      karmaCount: karmaCount,
      userVote: userVote
    );
  }
}
