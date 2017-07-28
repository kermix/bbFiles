using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Services
{
    public interface IMainDataAccessService
    {
        bbFiles.Entities.User LogIn(string username, string password);
    }
    public class MainDataAccessService : IMainDataAccessService
    {
        bbFiles.Entities.dbModel context = new bbFiles.Entities.dbModel();

        public bbFiles.Entities.User LogIn(string username, string password)
        {
            return context.Users
                .Where(x => x.Login.ToLower() == username.ToLower() && x.Password == password)
                .Select(x => x)
                .SingleOrDefault();
        }
    }
}
