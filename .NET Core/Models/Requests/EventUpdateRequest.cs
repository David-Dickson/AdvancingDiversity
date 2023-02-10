using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Models.Requests.Events
{
    public class EventUpdateRequest : EventAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
