import 'package:corsac_jwt/corsac_jwt.dart';

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
    if (expires == null)
      return true;

    return expires < DateTime.now().millisecondsSinceEpoch;
  }
}
