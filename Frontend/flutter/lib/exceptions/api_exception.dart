
import 'package:nanoblog/model/api_error.dart';

class ApiException implements Exception 
{
  String _message;

  ApiException(ApiError error)
  {
    _message = error.message;
  }

  String toString() => "ApiException: '$_message'";
}

