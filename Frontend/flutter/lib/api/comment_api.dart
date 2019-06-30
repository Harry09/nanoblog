import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/api/response/comment_response.dart';
import 'package:nanoblog/model/jwt.dart';

class CommentApi
{
  static Future<List<CommentResponse>> getComments(String entryId, {PagedQuery pagedQuery}) async
  {
    var url = "/comments/entry/$entryId";

    if (pagedQuery != null)
    {
      url += "?${pagedQuery.getQuery()}";
    }

    var result = await ApiBase.get(url);

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => CommentResponse.fromJson(i)).toList();
    }

    return null;
  }

  static Future<CommentResponse> getComment(String id) async
  {
    var result = await ApiBase.get("/comments/$id");

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return CommentResponse.fromJson(json);
    }

    return null;
  }

  static Future<CommentResponse> addComment(String entryId, String text, Jwt jwtToken) async
  {
    var messageBody = json.encode({
      "entryId": entryId,
      "text": text
    });

    var result = await ApiBase.post(
      "/comments",
      jsonBody: messageBody,
      token: jwtToken.token
    );

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return CommentResponse.fromJson(json);
    }

    return null;
  }

  static Future<bool> deleteComment(String id, Jwt jwtToken) async
  {
    var result = await ApiBase.delete("/comments/$id", token: jwtToken.token);
    
    return result.statusCode == 200;
  }
}