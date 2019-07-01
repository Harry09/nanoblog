import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/user.dart';

class EntryResponse
{
  String id;
  String authorId;
  String text;
  String createTime;

  int commentsCount;

  EntryResponse({this.id, this.authorId, this.text, this.createTime, this.commentsCount});

  EntryResponse.fromJson(Map<String, dynamic> json)
  {
    this.id = json['id'];
    this.authorId = json['authorId'];
    this.text = json['text'];
    this.createTime = json['createTime'];
    this.commentsCount = json['commentsCount'];
  }

  Entry toEntry(User author)
  {
    return Entry(
      id: id,
      author: author,
      text: text,
      commentsCount: commentsCount,
      createTime: createTime,
    );
  }
}
