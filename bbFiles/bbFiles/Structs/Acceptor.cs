using System.Linq;

namespace bbFiles.Structs
{
    class Acceptor : UserContactInfo
    {
        public string name { get; set; }
        public string adress { get; set; }

        public Acceptor()
        {
            name = "";
            adress = "";
            email = "";
            phone = null;
        }
        public Acceptor(string name, string adress, string email, int? phone)
        {
            this.name = name;
            this.adress = adress;
            this.email = email;
            this.phone = phone;
        }

        public void Add(User user)
        {
            var dc = new databaseDataContext();
            Acceptors newAcceptorRow = new Acceptors()
            {
                AcceptorName = this.name,
                Address = this.adress,
                Email = this.email,
                PhoneNumber = this.phone,
                UserID = (from c in dc.Credentials
                          where c.Login == user.username
                          select c.UserID).Single()
            };
            if (this.IsPhoneNumberValid() && this.IsEmailValid())
            {
                dc.Acceptors.InsertOnSubmit(newAcceptorRow);
                dc.SubmitChanges();
            }

            if (!((from c in dc.Acceptors
                   where c.UserID == newAcceptorRow.UserID
                   select c).Any()))
                user.Delete();
        }
        public void Edit(User user)
        {
            var dc = new databaseDataContext();
            int UserID = (from c in dc.Credentials
                          where c.Login == user.username
                          select c.UserID).SingleOrDefault();

            var q2 = (from c in dc.Acceptors
                      where c.UserID == UserID
                      select c).Single();

            if (q2 != null)
            {
                q2.AcceptorName = this.name;
                q2.Address = this.adress;
                if (this.IsPhoneNumberValid() && this.IsEmailValid())
                {
                    q2.Email = this.email;
                    q2.PhoneNumber = this.phone;
                }
            }
            dc.SubmitChanges();
        }
        public void Delete(User user)
        {
            var dc = new databaseDataContext();
            int UserID = (from c in dc.Credentials
                          where c.Login == user.username
                          select c.UserID).SingleOrDefault();

            var q2 = (from r in dc.Acceptors
                      where r.UserID == UserID
                      select r).SingleOrDefault();

            if (q2 != null)
                q2.UserID = -1;

            dc.SubmitChanges();
        }
    }
}

