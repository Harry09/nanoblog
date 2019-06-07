
class ApiError
{
  String message;

  ApiError(this.message);

  ApiError.fromJson(Map json)
  {
    this.message = json["message"];
  }
}
