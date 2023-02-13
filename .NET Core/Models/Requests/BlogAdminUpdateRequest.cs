using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.BlogsAdmin
{
  public class BlogAdminUpdateRequest:BlogAdminAddRequest, IModelIdentifier 
    {

        public int Id { get; set; }
      
    }
}
