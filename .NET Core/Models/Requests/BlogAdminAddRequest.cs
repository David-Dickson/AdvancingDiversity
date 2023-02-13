using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.BlogsAdmin
{
    public class BlogAdminAddRequest
    {
        public int BlogTypeId { get; set; }
        
        public int AuthorId { get; set; }
        
        public string Title { get; set; }
        
        public string Subject { get; set; }
        
        public string Content { get; set; }
        
        public bool IsPublished { get; set; }
        
        public string ImageUrl { get; set; }
	  }
}
