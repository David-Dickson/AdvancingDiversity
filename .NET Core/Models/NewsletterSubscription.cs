using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Domain.Newsletters
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool IsSubscribed { get; set; }

        public DateTime DateCreated { get; set;}

        public DateTime DateModified { get; set; }
    }
}
