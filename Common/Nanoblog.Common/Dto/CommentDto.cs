﻿using System;

namespace Nanoblog.Common.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }

        public AuthorDto Author { get; set; }

        public string ParentId { get; set; }

        public string Text { get; set; }

        public DateTime CreateTime { get; set; }

        public int KarmaCount { get; set; }

        public KarmaValue UserVote { get; set; }
    }
}
