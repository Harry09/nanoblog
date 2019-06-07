class Jwt
{
  String token;
  String refreshToken;
  BigInt expires;

  Jwt({this.token, this.refreshToken, this.expires});

  Jwt.fromJson(Map json)
  {
    this.token = json["token"];
    this.refreshToken = json["refreshToken"];
    this.expires = json["expires"];
  }
}
