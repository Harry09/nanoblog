
class Error
{
  String message;

  Error.fromJson(Map json)
  {
    this.message = json["message"];
  }
}
