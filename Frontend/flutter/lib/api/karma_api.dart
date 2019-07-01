import 'dart:convert';

import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/response/karma_response.dart';
import 'package:tuple/tuple.dart';

enum _VoteAction
{
  UpVote,
  DownVote,
  RemoveVote
}

class KarmaApi
{
  // select entryId or commentId
  static Future<List<KarmaResponse>> getKarma({String entryId, String commentId}) async
  {
    var tuple = _getServiceAndItemId(entryId: entryId, commentId: commentId);
    var service = tuple.item1;
    var itemId = tuple.item2;

    var result = await ApiBase.get("/karma/$service/$itemId");

    if (result.statusCode == 200)
    {
      var json = jsonDecode(result.body);

      return (json as List).map((i) => KarmaResponse.fromJson(i)).toList();
    }

    return null;
  }

  // select entryId or commentId
  static Future<bool> upVote({String entryId, String commentId}) async
  {
    return await _vote(_VoteAction.UpVote, entryId: entryId, commentId: commentId);
  }

  // select entryId or commentId
  static Future<bool> downVote({String entryId, String commentId}) async
  {
    return await _vote(_VoteAction.DownVote, entryId: entryId, commentId: commentId);
  }

  // select entryId or commentId
  static Future<bool> removeVote({String entryId, String commentId}) async
  {
    return await _vote(_VoteAction.RemoveVote, entryId: entryId, commentId: commentId);
  }

  static Future<bool> _vote(_VoteAction action, {String entryId, String commentId}) async
  {
    var actionString;

    switch (action)
    {
      case _VoteAction.UpVote:
        actionString = "upvote";
        break;
      case _VoteAction.DownVote:
        actionString = "downvote";
        break;
      case _VoteAction.RemoveVote:
        actionString = "remove";
        break;
    }

    var tuple = _getServiceAndItemId(entryId: entryId, commentId: commentId);
    var service = tuple.item1;
    var itemId = tuple.item2;

    var result = await ApiBase.get("/karma/$service/$actionString/$itemId");

    if (result.statusCode == 200)
    {
      return true;
    }

    return false;
  }

  static Tuple2<String, String> _getServiceAndItemId({String entryId, String commentId})
  {
    if (entryId != null && commentId != null)
    {
      throw new ArgumentError("Choose only one parameter");
    }
    else if (entryId != null)
    {
      return Tuple2<String, String>("entry", entryId);
    }
    else if (commentId != null)
    {
      return Tuple2<String, String>("comment", commentId);
    }
    else // if both null
    {
      throw new ArgumentError("Choose one parameter");
    }
  }
}