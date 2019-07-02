import 'package:flutter/cupertino.dart';
import 'package:nanoblog/api/karma_api.dart';
import 'package:nanoblog/model/jwt.dart';
import 'package:nanoblog/model/karma.dart';
import 'package:nanoblog/util/karma_value.dart';

class KarmaRepository
{
  KarmaRepository();

  // select entryId or commentId
  Future<List<Karma>> getKarma({String entryId, String commentId}) async
  {
    var karma = await KarmaApi.getKarma(entryId: entryId, commentId: commentId);

    return karma.map((i) => i.toKarma());
  }

  // select entryId or commentId
  Future<bool> upVote({String entryId, String commentId, @required Jwt jwtToken}) async
  {
    return await KarmaApi.upVote(entryId: entryId, commentId: commentId, jwtToken: jwtToken);
  }

  // select entryId or commentId
  Future<bool> downVote({String entryId, String commentId, @required Jwt jwtToken}) async
  {
    return await KarmaApi.downVote(entryId: entryId, commentId: commentId, jwtToken: jwtToken);
  }

  // select entryId or commentId
  Future<bool> removeVote({String entryId, String commentId, @required Jwt jwtToken}) async
  {
    return await KarmaApi.removeVote(entryId: entryId, commentId: commentId, jwtToken: jwtToken);
  }

  Future<int> countVotes({String entryId, String commentId}) async
  {
    return await KarmaApi.countVotes(entryId: entryId, commentId: commentId);
  }

  Future<KarmaValue> getUserVote({@required String userId, String entryId, String commentId}) async
  {
    return await KarmaApi.getUserVote(userId: userId, entryId: entryId, commentId: commentId);
  }
}