using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hidden.Models.Domain.Location;
using Hidden.Models.Domain.States;
using Hidden.Models.Domain.JobTypes;

namespace Hidden.Models.Domain.Jobs
{
    public class JobPublic
    {
        public int Id { get; set; }
        
        public JobType JobType { get; set; }
        
        public Location.Location Location { get; set; }
        
        public State State { get; set; }
        
        public int CreatedBy { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Requirements { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsRemote { get; set; }
        
        public string ContactName { get; set; }
        
        public string ContactPhone { get; set; }
        
        public string ContactEmail { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
    }
}
