import 'package:http/http.dart' as http;
import 'package:meta/meta.dart';


class ApiBase
{
  static const String baseUrl = "http://192.168.0.161:53188/api";

  static String _fixApiUrl(String apiUrl)
  {
    if (apiUrl[0] != '/')
    {
      apiUrl = "/" + apiUrl;
    }

    return apiUrl;
  }

  static Future<http.Response> get(String apiUrl, {Map<String, String> headers}) async
  {
    apiUrl = _fixApiUrl(apiUrl);

    return await http.get(baseUrl + apiUrl, headers: headers);
  }

  static Future<http.Response> post(String apiUrl, {@required String jsonBody, String token, Map<String, String> headers}) async
  {
    apiUrl = _fixApiUrl(apiUrl);

    var _headers = {
      "Content-Type": "application/json",
      "Accept": "application/json",
    };

    if (token != null)
    {
      _headers.addAll({
        "Authorization": "Bearer $token"
      });
    }

    if (headers != null)
    {
      _headers.addAll(headers);
    }

    return await http.post(
      baseUrl + apiUrl, 
      headers: _headers,
      body: jsonBody
    );
  }
}
