import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/exceptions/api_exception.dart';
import 'package:nanoblog/model/api_error.dart';
import 'package:nanoblog/model/jwt.dart';

class AccountApi
{
  static Future register(String email, String userName, String password) async
  {
    var body = json.encode({
      "email": email,
      "userName": userName,
      "password": password
    });

    var result = await ApiBase.post("/accounts/register", jsonBody: body);

    if (result.statusCode == 400)
    {
      var jsonData = json.decode(result.body);
      var apiError = ApiError.fromJson(jsonData);

      throw ApiException(apiError);
    }
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
      var jwt = Jwt.fromJson(jsonData);

      return jwt;
    }
    else if (result.statusCode == 400)
    {
      var jsonData = json.decode(result.body);
      var apiError = ApiError.fromJson(jsonData);

      throw ApiException(apiError);
    }

    return null;
  }
}