using System.Collections.ObjectModel;
using bbFiles.Entities;
using System.Data.Entity;

namespace bbFiles.Services
{
    public interface IUsersDataAccessService
    {
        ObservableCollection<User> GetUsers();
        int CreateUser(User User);
        void DeleteUser(User User);
    }
    public class UsersDataAccessService : IUsersDataAccessService
    {
        dbModel context = new dbModel();

        public ObservableCollection<User> GetUsers()
        {
            context.Dispose();
            context = new dbModel();
            context.Users.Load();
            ObservableCollection<User> Users = context.Users.Local;
            return Users;
        }

        public int CreateUser(User User)
        {
            context.Entry(User).State = User.Id == 0 ?
                                    EntityState.Added :
                                    EntityState.Modified;

            context.SaveChanges();
            return User.Id;
        }
        public void DeleteUser(User User)
        {
            context.Users.Attach(User);
            context.Users.Remove(User);
            context.SaveChanges();
        }
    }
}
