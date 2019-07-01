import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/user.dart';

class CommentResponse
{
  String id;
  String authorId;
  String parentId;
  String text;
  String createTime;

  CommentResponse({this.id, this.authorId, this.parentId, this.text, this.createTime});

  CommentResponse.fromJson(Map<String, dynamic> json)
  {
    id = json["id"];
    authorId = json["authorId"];
    parentId = json["parentId"];
    text = json["text"];
    createTime = json["createTime"];
  }

  Comment toComment(User author)
  {
    return Comment(
      id: id,
      author: author,
      text: text,
      createTime: createTime
    );
  }
}