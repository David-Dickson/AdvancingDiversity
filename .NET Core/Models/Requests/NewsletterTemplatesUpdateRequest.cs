using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.NewsletterTemplates
{
    public class NewsletterTemplatesUpdateRequest : NewsletterTemplatesInsertRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
