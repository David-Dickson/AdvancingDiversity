using Hidden.Models.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Blog
{
    public class Blogs 
    {
        public int Id { get; set; }
        
        public int BlogTypeId { get; set; }
        
        public string BlogTypeName { get; set; }
        
        public int AuthorId { get; set; }
        
        public string AuthorEmail { get; set; }
        
        public string Title { get; set; }
        
        public string Subject { get; set; }
        
        public string Content { get; set; }
        
        public bool IsPublished { get; set; }
        
        public string ImageUrl { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
    }
}
