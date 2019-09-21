using System;

namespace Nanoblog.Common.Dto
{
    public class AuthorDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public DateTime JoinTime { get; set; }
    }
}
