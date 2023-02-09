using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.Newsletters
{
    public class NewsletterSubscriptionAddRequest
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        public bool IsSubscribed { get; set; }
    }
}
