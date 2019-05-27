class User
{
  String id;
  String userName;
  String email;
  String role;
  String joinTime;

  User({String id, String userName, String email, String role, String joinTime})
  {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.role = role;
    this.joinTime = joinTime;
  }

  User.fromJson(Map json)
  {
    this.id = json["id"];
    this.userName = json["userName"];
    this.email = json["email"];
    this.role = json["role"];
    this.joinTime = json["joinTime"];
  }
}
