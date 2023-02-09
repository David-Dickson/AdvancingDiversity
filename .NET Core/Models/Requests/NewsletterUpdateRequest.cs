using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.Newsletters
{
    public class NewsletterUpdateRequest : NewsletterAddRequest, IModelIdentifier
    {
        public int Id { get; set; }

    }
}
