class User
{
  String id;
  String userName;
  String email;
  String role;
  String joinTime;

  User({this.id, this.userName, this.email, this.role, this.joinTime});

  User.fromJson(Map json)
  {
    this.id = json["id"];
    this.userName = json["userName"];
    this.email = json["email"];
    this.role = json["role"];
    this.joinTime = json["joinTime"];
  }
}
