using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Structs
{
    class UserDataContext
    {
        public User user { get; set; }
        public Acceptor acceptor { get; set; }
        public UserDataContext(User user, Acceptor acceptor)
        {
            this.user = user;
            this.acceptor = acceptor;
        }

        public void Add()
        {
            if (userExists(this.user.username))
                throw new UserEditException(Properties.Strings.UserExists);
            else
            {
                this.user.Add();
                if (this.acceptor.name != null)
                {
                    this.acceptor.Add(this.user);
                }
            }
        }
        public void Edit()
        {
            this.user.Edit();

            if (this.acceptor.name != null)
                this.acceptor.Edit(user);
        }

        public void Delete()
        {
            if (acceptor != null)
                acceptor.Delete(user);
            user.Delete();
        }

        private static bool userExists(string username)
        {
            databaseDataContext dc = new databaseDataContext();
            bool userExists = (from c in dc.Credentials
                               where c.Login == username
                               select c).Any();
            if (userExists)
                return true;

            return false;
        }
    }
}
