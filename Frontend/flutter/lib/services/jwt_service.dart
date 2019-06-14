import 'package:nanoblog/api/repository/account_repository.dart';
import 'package:nanoblog/model/jwt.dart';
import 'package:shared_preferences/shared_preferences.dart';

class JwtService
{
  Jwt _jwtToken;

  Jwt get jwtToken => _jwtToken;

  AccountRepository _accountRepository;

  JwtService(this._accountRepository);

  bool isExpired()
  {
    if (jwtToken != null)
      return jwtToken.isExpired();
    
    return true;
  }

  Future refreshToken() async
  {
    if (jwtToken == null)
      return;

    var result = await _accountRepository.refreshAccessToken(jwtToken.refreshToken);

    if (result != null)
    {
      _jwtToken = result;
    }
  }

  // refreshes token only when it is needed
  Future tryRefreshToken() async
  {
    if (isExpired())
      await refreshToken();
  }

  Future revokeToken() async
  {
    if (_jwtToken == null)
      return;

    await _accountRepository.revokeRefreshToken(_jwtToken.refreshToken);
  }

  Future setJwt(Jwt jwtToken) async
  {
    this._jwtToken = jwtToken;

    save();
  }

  Future resetToken() async
  {
    _jwtToken = null;

    _deleteSave();
  }

  Future save() async
  {
    if (_jwtToken == null)
      return;

    final prefs = await SharedPreferences.getInstance();
    prefs.setString("token", _jwtToken.token);
    prefs.setString("refreshToken", _jwtToken.refreshToken);
    prefs.setInt("expires", _jwtToken.expires);
  }

  Future load() async
  {
    final prefs = await SharedPreferences.getInstance();

    if (prefs.containsKey("token") == false ||
        prefs.containsKey("refreshToken") == false ||
        prefs.containsKey("expires") == false)
    {
      return;
    }

    _jwtToken = Jwt();

    _jwtToken.token = prefs.getString("token");
    _jwtToken.refreshToken = prefs.getString("refreshToken");
    _jwtToken.expires = prefs.getInt("expires");
  }

  Future _deleteSave() async
  {
    final prefs = await SharedPreferences.getInstance();
    prefs.remove("token");
    prefs.remove("refreshToken");
    prefs.remove("expires");
  }
}