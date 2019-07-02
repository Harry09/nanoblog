import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/util/karma_value.dart';

class CommentResponse
{
  String id;
  String authorId;
  String parentId;
  String text;
  String createTime;
  int karmaCount;
  KarmaValue userVote;

  CommentResponse({this.id, this.authorId, this.parentId, this.text, this.createTime});

  CommentResponse.fromJson(Map<String, dynamic> json)
  {
    id = json["id"];
    authorId = json["authorId"];
    parentId = json["parentId"];
    text = json["text"];
    createTime = json["createTime"];
    karmaCount = json['karmaCount'];
    userVote = getKarmaValueFromInt(json['userVote'] as int);
  }

  Comment toComment(User author)
  {
    return Comment(
      id: id,
      author: author,
      text: text,
      createTime: createTime,
      karmaCount: karmaCount,
      userVote: userVote
    );
  }
}