import 'package:nanoblog/api/response/author_response.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/util/karma_value.dart';

class CommentResponse
{
  String id;
  AuthorResponse author;
  String parentId;
  String text;
  String createTime;
  int karmaCount;
  KarmaValue userVote;

  CommentResponse({this.id, this.author, this.parentId, this.text, this.createTime});

  CommentResponse.fromJson(Map<String, dynamic> json)
  {
    id = json["id"];
    author = AuthorResponse.fromJson(json['author']);
    parentId = json["parentId"];
    text = json["text"];
    createTime = json["createTime"];
    karmaCount = json['karmaCount'];
    userVote = getKarmaValueFromInt(json['userVote'] as int);
  }

  Comment toComment()
  {
    return Comment(
      id: id,
      author: author.toAuthor(),
      text: text,
      createTime: createTime,
      karmaCount: karmaCount,
      userVote: userVote
    );
  }
}