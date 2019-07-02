import 'package:nanoblog/api/account_api.dart';
import 'package:nanoblog/api/response/jwt_response.dart';
import 'package:nanoblog/model/jwt.dart';
import 'package:nanoblog/model/user.dart';

class AccountRepository
{
  Map<String, User> _cache = Map<String, User>();

  AccountRepository();

  Future<bool> register(String email, String userName, String password) async
  {
    return await AccountApi.register(email, userName, password);
  }

  Future<Jwt> login(String email, String password) async
  {
    var result = await AccountApi.login(email, password);

    return _getJwt(result);
  }

  Future<User> getUser(String userId, {bool forceCacheUpdate = false}) async
  {
    if (_cache.containsKey(userId))
    {
      return _cache[userId];
    }

    var result = await AccountApi.getUser(userId);

    if (result == null)
      return null;

    return result.toUser();
  }

  Future<Jwt> refreshAccessToken(String refreshToken) async
  {
    var result = await AccountApi.refreshAccessToken(refreshToken);

    return _getJwt(result);
  }

  Future<bool> revokeRefreshToken(String refreshToken) async
  {
    return await AccountApi.revokeRefreshToken(refreshToken);
  }

  Jwt _getJwt(JwtResponse response)
  {
    if (response == null)
      return null;

    return Jwt(
      token: response.token,
      refreshToken: response.refreshToken,
      expires: response.expires
    );
  }
}