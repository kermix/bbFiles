using bbFiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Services
{
    public interface IStatisticsDataAccessService
    {
        long GetStatistic(BloodTypeMarker BloodType);
    }
    class StatisticsDataAccessService : IStatisticsDataAccessService
    {
        dbModel context = new dbModel();
        public long GetStatistic(BloodTypeMarker BloodType)
        {
            context.Dispose();
            context = new dbModel();
            return (context.Statistics.Find(BloodType)).TotalAmount / 5000;
        }
    }
}
