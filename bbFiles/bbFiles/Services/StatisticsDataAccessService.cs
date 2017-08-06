using bbFiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Services
{
    /// <exclude />
    public interface IStatisticsDataAccessService
    {
        long GetStatistic(BloodTypeMarker BloodType);
    }
    /// <summary>
    /// Serves connection and operations for statistics in db.
    /// </summary>
    class StatisticsDataAccessService : IStatisticsDataAccessService
    {
        dbModel context = new dbModel();
        /// <summary>
        /// Gets statistics value for <paramref name="BloodType"/> from db.
        /// </summary>
        /// <param name="BloodType">Type of the blood.</param>
        /// <returns>Percent of reserve for <paramref name="BloodType"/> 
        /// or throws an NullReferenceException if there is no such blood type in Statistics table.</returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public long GetStatistic(BloodTypeMarker BloodType)
        {
            try
            {
                context.Dispose();
                context = new dbModel();
                return (context.Statistics.Find(BloodType)).TotalAmount / 5000;
            }
            catch(NullReferenceException)
            {
                throw new NullReferenceException();
            }
        }
    }
}
