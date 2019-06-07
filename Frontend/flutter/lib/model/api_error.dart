
class ApiError
{
  String message;

  ApiError(this.message);

  ApiError.fromJson(Map<String, dynamic> json)
  {
    this.message = json["message"];
  }
}
