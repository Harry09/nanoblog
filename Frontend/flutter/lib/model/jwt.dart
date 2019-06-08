import 'package:corsac_jwt/corsac_jwt.dart';

class Jwt
{
  String token;
  String refreshToken;
  int expires;

  Jwt({this.token, this.refreshToken, this.expires});

  Jwt.fromJson(Map<String, dynamic> json)
  {
    this.token = json["token"];
    this.refreshToken = json["refreshToken"];
    this.expires = json["expires"];
  }

  String getUserId()
  {
    var jwt = JWT.parse(token);

    if (jwt != null)
    {
      return jwt.subject;
    }
    
    return null;
  }

  bool isExpired()
  {
    return expires < DateTime.now().millisecondsSinceEpoch;
  }
}
