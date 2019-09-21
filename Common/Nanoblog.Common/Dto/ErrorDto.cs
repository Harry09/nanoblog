namespace Nanoblog.Common.Dto
{
    public class ErrorDto
    {
        public string Message { get; private set; }

        public ErrorDto(string message)
        {
            this.Message = message;
        }
    }
}
