class EntryResponse
{
  String id;
  String authorId;
  String text;
  String createTime;

  EntryResponse({this.id, this.authorId, this.text, this.createTime});

  EntryResponse.fromJson(Map<String, dynamic> json)
  {
    this.id = json['id'];
    this.authorId = json['authorId'];
    this.text = json['text'];
    this.createTime = json['createTime'];
  }
}
