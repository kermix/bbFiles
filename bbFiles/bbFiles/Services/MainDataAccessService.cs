using bbFiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Services
{
    public interface IMainDataAccessService
    {
        User LogIn(string username, string password);
    }
    public class MainDataAccessService : IMainDataAccessService
    {
        public bbFiles.Entities.User LogIn(string username, string password)
        {
            using(var context = new dbModel())
            {
                return context.Users
                .Where(x => x.Login.ToLower() == username.ToLower() && x.Password == password)
                .Select(x => x)
                .SingleOrDefault();
            }
        }
    }
}
