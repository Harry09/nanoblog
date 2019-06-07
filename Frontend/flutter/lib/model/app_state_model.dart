import 'package:nanoblog/model/jwt.dart';
import 'package:nanoblog/model/user.dart';
import 'package:scoped_model/scoped_model.dart';

class AppStateModel extends Model
{
  Jwt jwtToken;
  User currentUser;
}