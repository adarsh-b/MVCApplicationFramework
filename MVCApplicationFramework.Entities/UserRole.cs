using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApplicationFramework.Entities
{
    public partial class UserRole
    {
        public UserRole()
        {
            this.Roles = new List<Role>();
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
