using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
