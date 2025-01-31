using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BlogPost
    {
        public string BlogId { get; set; }
        public string CreatedUserId { get; set; }
        public string Title { get; set; }
        public List<Section> Content {  get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> Category {  get; set; }
        public BlogStatus BlogStatus{ get; set; }
        public string S3Cover { get; set; }    
    }
    public class Section
    {
        public int Sequences { get; set; }
        public List<Font> Text { get; set; }
        public string? Image { get; set; }
        public string SnippetCode { get; set; }
        public Justify JustifyContent { get; set; }
    }
    public class Font
    {
        public string Content { get; set; }
        public string FontSize { get; set; }
        public Stye FontStye { get; set; }

    }
}
