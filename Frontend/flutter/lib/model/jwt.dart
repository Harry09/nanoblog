class Jwt
{
  String token;
  String refreshToken;
  BigInt expires;

  Jwt.fromJson(Map json)
  {
    this.token = json["token"];
    this.refreshToken = json["refreshToken"];
    this.expires = json["expires"];
  }
}
