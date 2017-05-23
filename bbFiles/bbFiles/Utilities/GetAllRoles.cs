using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles
{
    partial class Utilities
    {
        public static Dictionary<Roles, string> GetAllRoles()
        {
            Dictionary<Roles, string> RolesDictionary = new Dictionary<Roles, string>();
            RolesDictionary.Add(Roles.ACCEPTOR, Properties.Strings.Acceptor);
            RolesDictionary.Add(Roles.EMPLOYEE, Properties.Strings.Employee);
            RolesDictionary.Add(Roles.ADMIN, Properties.Strings.Admin);
            return RolesDictionary;
        }
    }
}
