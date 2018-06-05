using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.Entities
{
    public class AdminUserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}
