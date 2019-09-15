import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/response/jwt_response.dart';
import 'package:nanoblog/api/response/user_response.dart';
import 'package:nanoblog/model/jwt.dart';
import 'package:nanoblog/model/user.dart';

class AccountApi
{
  static Future<bool> register(String email, String userName, String password) async
  {
    var body = json.encode({
      "email": email,
      "userName": userName,
      "password": password
    });

    var result = await ApiBase.post("/accounts/register", jsonBody: body);

    if (result.statusCode == 200)
    {
      return true;
    }

    return false;
  }

  static Future<Jwt> login(String email, String password) async
  {
    var body = json.encode({
      "email": email,
      "password": password
    });

    var result = await ApiBase.post("/accounts/login", jsonBody: body);

    if (result.statusCode == 200)
    {
      var jsonData = json.decode(result.body);
      
      return JwtResponse.fromJson(jsonData).toJwt();
    }

    return null;
  }

  static Future<User> getUser(String userId) async
  {
    var result = await ApiBase.get("/accounts/user/byId/$userId");

    if (result.statusCode == 200)
    {
      var jsonData = json.decode(result.body);

      return UserResponse.fromJson(jsonData).toUser();
    }
    
    return null;
  }

  static Future<Jwt> refreshAccessToken(String refreshToken) async
  {
    var result = await ApiBase.get("/accounts/tokens/refresh/$refreshToken");

    if (result.statusCode == 200)
    {
      var jsonData = json.decode(result.body);

      return JwtResponse.fromJson(jsonData).toJwt();
    }

    return null;
  }

  static Future<bool> revokeRefreshToken(String refreshToken) async
  {
    var result = await ApiBase.get("/accounts/tokens/revoke/$refreshToken");

    return result.statusCode == 200;
  }
}