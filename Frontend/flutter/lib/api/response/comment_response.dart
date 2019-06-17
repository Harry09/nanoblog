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
}