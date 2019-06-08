import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/services/jwt_service.dart';
import 'package:scoped_model/scoped_model.dart';

class AppStateModel extends Model
{
  JwtService jwtService;
  User currentUser;
}