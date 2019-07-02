import 'dart:convert';

import 'package:flutter/widgets.dart';
import 'package:http/http.dart';
import 'package:nanoblog/api/api_base.dart';
import 'package:nanoblog/api/response/karma_response.dart';
import 'package:nanoblog/model/jwt.dart';
import 'package:nanoblog/util/karma_value.dart';
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
  static Future<bool> upVote({String entryId, String commentId, @required Jwt jwtToken}) async
  {
    return await _vote(_VoteAction.UpVote, entryId: entryId, commentId: commentId, jwtToken: jwtToken);
  }

  // select entryId or commentId
  static Future<bool> downVote({String entryId, String commentId, @required Jwt jwtToken}) async
  {
    return await _vote(_VoteAction.DownVote, entryId: entryId, commentId: commentId, jwtToken: jwtToken);
  }

  // select entryId or commentId
  static Future<bool> removeVote({String entryId, String commentId, @required Jwt jwtToken}) async
  {
    return await _vote(_VoteAction.RemoveVote, entryId: entryId, commentId: commentId, jwtToken: jwtToken);
  }

  static Future<int> countVotes({String entryId, String commentId}) async
  {
    var tuple = _getServiceAndItemId(entryId: entryId, commentId: commentId);
    var service = tuple.item1;
    var itemId = tuple.item2;

    var result = await ApiBase.get("/karma/$service/count/$itemId");

    if (result.statusCode == 200)
    {
      return int.parse(result.body);
    }

    return null;
  }

  static Future<KarmaValue> getUserVote({@required String userId, String entryId, String commentId}) async
  {
    var tuple = _getServiceAndItemId(entryId: entryId, commentId: commentId);
    var service = tuple.item1;
    var itemId = tuple.item2;

    var result = await ApiBase.get("/karma/$service/?userId=$userId&itemId=$itemId");

    if (result.statusCode == 200)
    {
      return getKarmaValueFromInt(int.parse(result.body));
    }

    return null;
  }

  static Future<bool> _vote(_VoteAction action, {String entryId, String commentId, Jwt jwtToken}) async
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

    Response result;

    if (action == _VoteAction.RemoveVote)
    {
      result = await ApiBase.delete("/karma/$service/$actionString/$itemId", jwtToken: jwtToken.token);
    }
    else
    {
      result = await ApiBase.get("/karma/$service/$actionString/$itemId", jwtToken: jwtToken.token);
    }

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