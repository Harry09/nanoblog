import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/model/entry.dart';
import 'package:http/http.dart' as http;

class EntryApi extends ApiBase 
{
  static Future<List<Entry>> getEntries() async
  {
    String url = "${ApiBase.baseUrl}/entries";
    
    var result = await http.get(url);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => Entry.fromJson(i)).toList();
    }

    return null;
  }

  static Future<Entry> getEntry(String id) async
  {
    String url = "${ApiBase.baseUrl}/entries/$id";

    var result = await http.get(url);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return Entry.fromJson(json);
    }
    
  }
}
