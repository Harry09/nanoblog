class Jwt
{
  String token;
  String refreshToken;
  BigInt expires;

  Jwt({String token, String refreshToken, BigInt expires})
  {
    this.token = token;
    this.refreshToken = refreshToken;
    this.expires = expires;
  }

  Jwt.fromJson(Map json)
  {
    this.token = json["token"];
    this.refreshToken = json["refreshToken"];
    this.expires = json["expires"];
  }
}
