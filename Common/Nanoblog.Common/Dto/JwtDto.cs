namespace Nanoblog.Common.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public long Expires { get; set; }
    }
}
