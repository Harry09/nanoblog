
class Error
{
  String message;

  Error(String message)
  {
    this.message = message;
  }

  Error.fromJson(Map json)
  {
    this.message = json["message"];
  }
}
