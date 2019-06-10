import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:nanoblog/api/account_api.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:nanoblog/screens/loading.dart';
import 'package:scoped_model/scoped_model.dart';

class StartupPage extends StatefulWidget
{
  @override
  _StartupPageState createState() => _StartupPageState();
}

class _StartupPageState extends State<StartupPage>
{
  AppStateModel _model;

  Future<bool> _toProcess() async
  {
    await _model.jwtService.load();

    if (_model.jwtService.jwtToken != null)
    {
      try
      {
        await _model.jwtService.tryRefreshToken();

        if (_model.jwtService.isExpired())
          return false;

        var user = await AccountApi.getUser(_model.jwtService.jwtToken.getUserId());

        _model.currentUser = user;

        return true;
      }
      on Exception
      {
        return false;
      }
    }

    return false;
  }

  void _onSuccess(BuildContext context)
  {
    Navigator.pushNamedAndRemoveUntil(context, "/home", (_) => false);
  }

  void _onFail(BuildContext context)
  {
    Navigator.pushNamedAndRemoveUntil(context, "/login", (_) => false);
  }

  @override
  Widget build(BuildContext context)
  {
    _model = ScopedModel.of(context, rebuildOnChange: true);

    return LoadingPage(
      message: "Logging in...",
      toProcess: _toProcess,
      onSuccess: () => _onSuccess(context),
      onFail: () => _onFail(context),
    );
  }
}