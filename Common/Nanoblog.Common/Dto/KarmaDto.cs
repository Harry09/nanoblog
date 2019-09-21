namespace Nanoblog.Common.Dto
{
    public class KarmaDto
    {
        public string Id { get; set; }

        public UserDto Author { get; set; }

        public KarmaValue Value { get; set; }
    }
}
