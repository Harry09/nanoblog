import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/response/jwt_response.dart';
import 'package:nanoblog/api/response/user_response.dart';

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

  static Future<JwtResponse> login(String email, String password) async
  {
    var body = json.encode({
      "email": email,
      "password": password
    });

    var result = await ApiBase.post("/accounts/login", jsonBody: body);

    if (result.statusCode == 200)
    {
      var jsonData = json.decode(result.body);
      
      return JwtResponse.fromJson(jsonData);
    }

    return null;
  }

  static Future<UserResponse> getUser(String userId) async
  {
    var result = await ApiBase.get("/accounts/user/byId/$userId");

    if (result.statusCode == 200)
    {
      var jsonData = json.decode(result.body);

      return UserResponse.fromJson(jsonData);
    }
    
    return null;
  }

  static Future<JwtResponse> refreshAccessToken(String refreshToken) async
  {
    var result = await ApiBase.get("/accounts/tokens/refresh/$refreshToken");

    if (result.statusCode == 200)
    {
      var jsonData = json.decode(result.body);

      return JwtResponse.fromJson(jsonData);
    }

    return null;
  }

  static Future<bool> revokeRefreshToken(String refreshToken) async
  {
    var result = await ApiBase.get("/accounts/tokens/revoke/$refreshToken");

    return result.statusCode == 200;
  }
}