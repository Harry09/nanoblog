import 'package:flutter/material.dart';
import 'package:nanoblog/app.dart';
import 'package:nanoblog/model/app_state_model.dart';
import 'package:scoped_model/scoped_model.dart';
import 'package:flutter/rendering.dart';

void main() 
{
  //debugPaintSizeEnabled=true;
 
  AppStateModel model = AppStateModel();

  runApp(ScopedModel<AppStateModel>(
    model: model,
    child: NanoblogApp(),
  ));
}
