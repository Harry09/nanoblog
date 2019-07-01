import 'package:nanoblog/api/karma_api.dart';
import 'package:nanoblog/model/karma.dart';

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
  Future<bool> upVote({String entryId, String commentId}) async
  {
    return await KarmaApi.upVote(entryId: entryId, commentId: commentId);
  }

  // select entryId or commentId
  Future<bool> downVote({String entryId, String commentId}) async
  {
    return await KarmaApi.downVote(entryId: entryId, commentId: commentId);
  }

  // select entryId or commentId
  Future<bool> removeVote({String entryId, String commentId}) async
  {
    return await KarmaApi.removeVote(entryId: entryId, commentId: commentId);
  }
}