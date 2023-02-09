using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.NewsletterTemplates
{
    public class NewsletterTemplatesInsertRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; } 
        
        [Required]
        public string PrimaryImage { get; set; }
                
    }
}
