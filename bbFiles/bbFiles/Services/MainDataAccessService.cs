using bbFiles.Entities;
using bbFiles.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Services
{
    /// <exclude />
    public interface IMainDataAccessService
    {
        User LogIn(string username, string password);
    }
    /// <summary>
    /// Service that serves the login operation.
    /// </summary>
    public class MainDataAccessService : IMainDataAccessService
    {
        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Logged user or null if nothing matched. Throws an EntityException on connection error.</returns>
        public bbFiles.Entities.User LogIn(string username, string password)
        {
            try
            {
                using (var context = new dbModel())
                {
                    return context.Users
                    .Where(x => x.Login.ToLower() == username.ToLower() && x.Password == password)
                    .Select(x => x)
                    .SingleOrDefault();
                }
            }
            catch (EntityException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
