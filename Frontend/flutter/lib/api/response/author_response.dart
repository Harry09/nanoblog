import 'package:nanoblog/model/author.dart';

class AuthorResponse
{
  String id;
  String userName;
  String role;
  String joinTime;

  AuthorResponse({this.id, this.userName, this.role, this.joinTime});

  AuthorResponse.fromJson(Map<String, dynamic> json)
  {
    this.id = json["id"];
    this.userName = json["userName"];
    this.role = json["role"];
    this.joinTime = json["joinTime"];
  }

  Author toAuthor()
  {
    return Author( 
      id: id,
      joinTime: joinTime,
      role: role,
      userName: userName
    );
  }
}
