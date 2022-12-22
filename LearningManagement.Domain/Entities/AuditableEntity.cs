using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Domain.Entities
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
    }
}
