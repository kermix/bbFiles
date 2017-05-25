using System;
using System.Linq;

namespace bbFiles.Structs
{
    class User
    {
        private const int minPasswordLength = 8;
        public string username { get; set; }
        public string password { get; set; }
        public Roles role { get; set; }
        private bool? _hasToChangePassword;
        public bool? hasToChangePassword
        {
            get
            {
                return (_hasToChangePassword != null) ? _hasToChangePassword : false;
            }
            set
            {
                _hasToChangePassword = value;
            }
        }
        public DateTime registeredDate { get; }
        public DateTime? lastLoggedDate { get; }
        public User()
        {
            username = "";
            password = "";
            role = Roles.ACCEPTOR;
            registeredDate = DateTime.Now;
            lastLoggedDate = null;
            hasToChangePassword = true;
        }
        public User(string username, string password, Roles role, DateTime registeredDate,
            DateTime? lastLoggedDate)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.registeredDate = registeredDate;
            this.lastLoggedDate = lastLoggedDate;
            this.hasToChangePassword = true;
        }
        public User(string username, string password, Roles role, DateTime registeredDate,
            DateTime? lastLoggedDate, bool? hasToChangePassword)
                : this(username, password, role, registeredDate, lastLoggedDate)
        {
            this.hasToChangePassword = hasToChangePassword;
        }

        public void Add()
        {
            databaseDataContext dc = new databaseDataContext();
            Credentials newUserRow = new Credentials
            {
                Login = this.username,
                Password = this.password,
                PasswordChanged = (bool)!this.hasToChangePassword,
                RegisteredDate = DateTime.Now,
                Role = (Roles)this.role
            };
            if (IsPasswordLengthProper())
            {
                dc.Credentials.InsertOnSubmit(newUserRow);
                dc.SubmitChanges();
            }
            else
                throw new ArgumentOutOfRangeException(Properties.Strings.PasswordTooShort);
        }

        public void Edit()
        {
            var dc = new databaseDataContext();
            var q = (from r in dc.Credentials
                     where r.Login == this.username
                     select r).Single();

            q.Password = this.password;
            q.PasswordChanged = (bool)!this.hasToChangePassword;
            if (IsPasswordLengthProper())
            {
                dc.SubmitChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException(Properties.Strings.PasswordTooShort);
            }
        }

        public void Delete()
        {
            databaseDataContext dc = new databaseDataContext();

            var q = (from r in dc.Credentials
                     where r.Login == this.username
                     select r).SingleOrDefault();
            dc.Credentials.DeleteOnSubmit(q);
            dc.SubmitChanges();
        }
        public bool IsPasswordLengthProper()
        {
            if (this.password.Length < minPasswordLength)
                return false;

            return true;
        }
    }
}
