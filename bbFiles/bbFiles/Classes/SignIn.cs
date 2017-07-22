using System;
using System.Linq;

namespace bbFiles
{
    class SignIn
    {
        public static User Verify(string username, string password)
        {
            try
            {
                databaseContext dc = new databaseContext();
                var q = (from r in dc.Credentials
                         where r.Login == username && r.Password == password
                         select r).SingleOrDefault();
                if (q != null)
                {
                    User user = new User(q.Login);
                    q.LastLoggedDate = DateTime.Now;
                    dc.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
