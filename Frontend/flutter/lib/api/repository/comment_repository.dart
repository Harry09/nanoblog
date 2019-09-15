import 'package:nanoblog/api/comment_api.dart';
import 'package:nanoblog/api/repository/account_repository.dart';
import 'package:nanoblog/api/requests/paged_query.dart';
import 'package:nanoblog/api/response/comment_response.dart';
import 'package:nanoblog/model/comment.dart';
import 'package:nanoblog/model/jwt.dart';

class CommentRepository
{
  AccountRepository _accountRepository;

  CommentRepository(this._accountRepository);

  Future<List<Comment>> getComments(String entryId, {PagedQuery pagedQuery, Jwt jwtToken}) async
  {
    var comments = await CommentApi.getComments(entryId, pagedQuery: pagedQuery, jwtToken: jwtToken);

    if (comments == null)
      return null;

    var result = List<Comment>(comments.length);

    int i = 0;

    for (var comment in comments)
    {
      result[i++] = await _getComment(comment);
    }

    return result;
  }

  Future<Comment> getComment(String id, {Jwt jwtToken}) async
  {
    var result = await CommentApi.getComment(id, jwtToken: jwtToken);

    if (result == null)
      return null;

    return _getComment(result);
  }

  Future<Comment> addComment(String entryId, String text, Jwt jwtToken) async
  {
    var result = await CommentApi.addComment(entryId, text, jwtToken);

    if (result == null)
      return null;

    return _getComment(result);
  }

  Future<bool> deleteComment(String id, Jwt jwtToken) async
  {
    return await CommentApi.deleteComment(id, jwtToken);
  }

  Future<Comment> _getComment(CommentResponse comment) async
  {
    return comment.toComment();
  }
}