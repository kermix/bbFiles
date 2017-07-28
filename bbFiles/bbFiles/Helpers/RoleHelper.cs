using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Helpers
{
    class RoleHelper
    {
        public static Dictionary<Role, string> GetAllRoles()
        {
            Dictionary<Role, string> RolesDictionary = new Dictionary<Role, string>();
            RolesDictionary.Add(Role.Acceptor, Resources.Strings.Acceptor);
            RolesDictionary.Add(Role.Worker, Resources.Strings.Worker);
            RolesDictionary.Add(Role.Admin, Resources.Strings.Admin);
            return RolesDictionary;
        }
    }
}
