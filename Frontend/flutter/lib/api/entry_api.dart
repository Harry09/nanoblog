import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/model/entry.dart';

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
    else if (result.statusCode == 400)
    {
      ApiBase.handleApiError(result.body);
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
    else if (result.statusCode == 400)
    {
      ApiBase.handleApiError(result.body);
    }
    
    return null;
  }
}
