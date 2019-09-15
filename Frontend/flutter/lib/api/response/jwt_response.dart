import 'package:corsac_jwt/corsac_jwt.dart';
import 'package:nanoblog/model/jwt.dart';

class JwtResponse
{
  String token;
  String refreshToken;
  int expires;

  JwtResponse({this.token, this.refreshToken, this.expires});

  JwtResponse.fromJson(Map<String, dynamic> json)
  {
    this.token = json["token"];
    this.refreshToken = json["refreshToken"];
    this.expires = json["expires"];
  }

  Jwt toJwt()
  {
    return Jwt(
      token: token,
      refreshToken: refreshToken,
      expires: expires
    );
  }
}
