import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/api/response/comment_response.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/jwt.dart';

class CommentApi
{
  static Future<List<Comment>> getComments(String entryId, {PagedQuery pagedQuery, Jwt jwtToken}) async
  {
    var url = "/comments/entry/$entryId";

    if (pagedQuery != null)
    {
      url += "?${pagedQuery.getQuery()}";
    }

    var result = await ApiBase.get(url, jwtToken: jwtToken?.token);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => CommentResponse.fromJson(i).toComment()).toList();
    }

    return null;
  }

  static Future<Comment> getComment(String id, {Jwt jwtToken}) async
  {
    var result = await ApiBase.get("/comments/$id", jwtToken: jwtToken?.token);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return CommentResponse.fromJson(json).toComment();
    }

    return null;
  }

  static Future<Comment> addComment(String entryId, String text, Jwt jwtToken) async
  {
    var messageBody = json.encode({
      "entryId": entryId,
      "text": text
    });

    var result = await ApiBase.post(
      "/comments",
      jsonBody: messageBody,
      jwtToken: jwtToken.token
    );

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return CommentResponse.fromJson(json).toComment();
    }

    return null;
  }

  static Future<bool> deleteComment(String id, Jwt jwtToken) async
  {
    var result = await ApiBase.delete("/comments/$id", jwtToken: jwtToken.token);
    
    return result.statusCode == 200;
  }
}