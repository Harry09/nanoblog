import 'package:nanoblog/api/repository/account_repository.dart';
import 'package:nanoblog/api/repository/entry_repository.dart';
import 'package:nanoblog/model/user.dart';
import 'package:nanoblog/services/jwt_service.dart';
import 'package:scoped_model/scoped_model.dart';

class AppStateModel extends Model
{
  AccountRepository accountRepository;
  EntryRepository entryRepository;
  JwtService jwtService;
  User currentUser;

  AppStateModel()
  {
    accountRepository = AccountRepository();
    entryRepository = EntryRepository(accountRepository);
    jwtService = JwtService(accountRepository);
  }
}