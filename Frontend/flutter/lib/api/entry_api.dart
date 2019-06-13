import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:nanoblog/model/jwt.dart';

class EntryApi 
{
  static Future<List<Entry>> getEntries() async
  {
    var result = await ApiBase.get("/entries");

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => Entry.fromJson(i)).toList();
    }

    return null;
  }

  static Future<Entry> getEntry(String id) async
  {
    var result = await ApiBase.get("/entries/$id");

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return Entry.fromJson(json);
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
      token: jwtToken.token
      );

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return Entry.fromJson(json);
    }

    return null;
  }

  static Future<bool> deleteEntry(String id, Jwt jwtToken) async
  {
    var result = await ApiBase.delete("/entries/$id", token: jwtToken.token);

    return result.statusCode == 200;
  }    
}
