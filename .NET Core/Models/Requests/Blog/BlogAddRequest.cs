using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Blog
{
    public class BlogAddRequest
    {
        [Required]
        public int BlogTypeId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(120)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(120)]
        public string Subject { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string ImageUrl { get; set; }

    }
}
