import 'package:nanoblog/model/user.dart';

class UserResponse
{
  String id;
  String userName;
  String email;
  String role;
  String joinTime;

  UserResponse({this.id, this.userName, this.email, this.role, this.joinTime});

  UserResponse.fromJson(Map<String, dynamic> json)
  {
    this.id = json["id"];
    this.userName = json["userName"];
    this.email = json["email"];
    this.role = json["role"];
    this.joinTime = json["joinTime"];
  }

  User toUser()
  {
    return User(
      email: email, 
      id: id,
      joinTime: joinTime,
      role: role,
      userName: userName
    );
  }
}
