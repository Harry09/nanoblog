import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/api/response/entry_response.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/jwt.dart';

class EntryApi 
{
  static Future<List<Entry>> getNewest({Jwt jwtToken, PagedQuery pagedQuery}) async
  {
    var url = "/entries/newest";

    if (pagedQuery != null)
    {
      url += "?${pagedQuery.getQuery()}";
    }

    var result = await ApiBase.get(url, jwtToken: jwtToken?.token);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => EntryResponse.fromJson(i).toEntry()).toList();
    }

    return null;
  }

  static Future<Entry> getEntry(String id, {Jwt jwtToken}) async
  {
    var result = await ApiBase.get("/entries/$id", jwtToken: jwtToken?.token);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body); 

      return EntryResponse.fromJson(json).toEntry();
    }
    
    return null;
  }

  static Future<Entry> addEntry(String text, Jwt jwtToken) async
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

      return EntryResponse.fromJson(json).toEntry();
    }

    return null;
  }

  static Future<bool> deleteEntry(String id, Jwt jwtToken) async
  {
    var result = await ApiBase.delete("/entries/$id", jwtToken: jwtToken.token);

    return result.statusCode == 200;
  }    
}
