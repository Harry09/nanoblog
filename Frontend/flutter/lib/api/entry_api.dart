import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/api/response/entry_response.dart';
import 'package:nanoblog/model/jwt.dart';

class EntryApi 
{
  static Future<List<EntryResponse>> getNewest({PagedQuery pagedQuery}) async
  {
    var url = "/entries/newest";

    if (pagedQuery != null)
    {
      url += "?${pagedQuery.getQuery()}";
    }

    var result = await ApiBase.get(url);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => EntryResponse.fromJson(i)).toList();
    }

    return null;
  }

  static Future<EntryResponse> getEntry(String id) async
  {
    var result = await ApiBase.get("/entries/$id");

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body); 

      return EntryResponse.fromJson(json);
    }
    
    return null;
  }

  static Future<EntryResponse> addEntry(String text, Jwt jwtToken) async
  {
    var jsonBody = json.encode({
      "text": text
    });

    var result = await ApiBase.post(
      "/entries",
      jsonBody: jsonBody,
      jwtToken: jwtToken.token
      );

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return EntryResponse.fromJson(json);
    }

    return null;
  }

  static Future<bool> deleteEntry(String id, Jwt jwtToken) async
  {
    var result = await ApiBase.delete("/entries/$id", jwtToken: jwtToken.token);

    return result.statusCode == 200;
  }    
}
