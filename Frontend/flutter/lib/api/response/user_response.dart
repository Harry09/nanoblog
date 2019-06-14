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
}
