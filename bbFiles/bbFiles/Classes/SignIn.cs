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
                databaseDataContext dc = new databaseDataContext();
                var q = (from r in dc.Credentials
                         where r.Login == username && r.Password == password
                         select r).SingleOrDefault();
                if (q != null)
                {
                    User user = new User(q.Login);
                    q.LastLoggedDate = DateTime.Now;
                    dc.SubmitChanges();
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
