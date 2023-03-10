using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Domain.NewsletterTemplates
{
    public class NewsletterTemplates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryImage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
    }
}
