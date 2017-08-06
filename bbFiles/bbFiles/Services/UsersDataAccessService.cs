using System.Collections.ObjectModel;
using bbFiles.Entities;
using System.Data.Entity;
using System.Linq;

namespace bbFiles.Services
{
    /// <exclude />
    public interface IUsersDataAccessService
    {
        ObservableCollection<User> GetUsers();
        int CreateUser(User User);
        void DeleteUser(User User);
    }
    /// <summary>
    /// Serves connection and operations for Users in db.
    /// </summary>
    /// <seealso cref="bbFiles.Services.IUsersDataAccessService" />
    public class UsersDataAccessService : IUsersDataAccessService
    {
        dbModel context = new dbModel();
        /// <summary>
        /// Gets all users from the db.
        /// </summary>
        /// <returns>Observable collection of all users</returns>
        public ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> Users = new ObservableCollection<User>();
            context.Dispose();
            context = new dbModel();
            context.Users.Load();
            Users = context.Users.Local;
            return Users;
        }

        /// <summary>
        /// Adds or edits <paramref name="User"/> to the db.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>ID of creates users.</returns>
        public int CreateUser(User User)
        {

            if ((User.Id == 0) &&
                (context.Users.FirstOrDefault(x => x.Login == User.Login) != default(User)))
                return 0;

            context.Entry(User).State = User.Id == 0 ?
                                    EntityState.Added :
                                    EntityState.Modified;
            
            context.SaveChanges();
            return User.Id;
        }
        /// <summary>
        /// Deletes <paramref name="User"/> from the db.
        /// </summary>
        /// <param name="User">The user.</param>
        public void DeleteUser(User User)
        {
            context.Users.Attach(User);
            context.Users.Remove(User);
            context.SaveChanges();
        }
    }
}
