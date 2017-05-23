using System;
using System.Linq;
using System.Security.Principal;

namespace bbFiles
{
    public class User : IIdentity, IPrincipal
    {
        private string name;
        public User(string name)
        {
            this.name = name;
        }
        public string AuthenticationType { get { return "Custom"; } }
        public bool IsAuthenticated { get { return true; } }
        public string Name { get { return this.name; } }
        public IIdentity Identity { get { return this; } }
        public bool IsInRole(string role)
        {
            databaseDataContext dc = new databaseDataContext();
            var q = (from r in dc.Credentials
                     where r.Login == name
                     select r.Role).SingleOrDefault();
            if ((role.ToLower() == "admin") && (q == Roles.ADMIN))
                return true;
            else if ((role.ToLower() == "employee") && (q == Roles.EMPLOYEE))
                return true;
            else if ((role.ToLower() == "acceptor") && (q == Roles.ACCEPTOR))
                return true;
            else
                return false;
        }
        public bool HasPasswordChanged()
        {
            try
            {
                databaseDataContext dc = new databaseDataContext();
                var q = (from r in dc.Credentials
                         where r.Login == name
                         select r.PasswordChanged).SingleOrDefault();
                if (q)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ChangePassword(string password)
        {
            try
            {
                databaseDataContext dc = new databaseDataContext();
                var q = (from r in dc.Credentials
                         where r.Login == name
                         select r).SingleOrDefault();
                q.Password = password;
                q.PasswordChanged = true;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
