using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.Newsletters
{
    public  class NewsletterAddRequest
    {
            [Required]
            public int TemplateId { get; set; }
            
            [Required]
            public string Name { get; set; }
            
            [Required]
            public string CoverPhoto { get; set; }
            
            [Required]
            public DateTime DateToPublish { get; set; }
            
            [Required]
            public DateTime DateToExpire { get; set; }
    }
}
