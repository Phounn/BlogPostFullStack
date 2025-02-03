using Share.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{
    public class BlogPost
    {
        public string? BlogId { get; set; }
        public string CreatedUserId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public List<Section>? Content { get; set; } = new();
        public DateTime CreatedDate { get; set; }
        public List<string>? Categories { get; set; } 
        public string? Category { get; set; }
        public BlogStatus BlogStatus { get; set; }
        public string S3Cover { get; set; } = default!;
    }
    public class Section
    {
        public int Sequences { get; set; }
        public string? Text { get; set; }
        public string? Image { get; set; }
        public string? SnippetCode { get; set; }
        public _ContentType ContentType { get; set; }
    }
}
