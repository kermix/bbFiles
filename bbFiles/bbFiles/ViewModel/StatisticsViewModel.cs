using bbFiles.Services;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace bbFiles.ViewModel
{
    public class StatisticsViewModel : ViewModelBase
    {
        IStatisticsDataAccessService _proxyService;
        Dictionary<BloodTypeMarker, int> _bloodTypes;
        public Dictionary<BloodTypeMarker, int> BloodTypes
        {
            get { return _bloodTypes; }
            set { _bloodTypes = value; RaisePropertyChanged("BloodTypes"); }
        }

        public StatisticsViewModel(IStatisticsDataAccessService proxyService)
        {
            _proxyService = proxyService;
            BloodTypes = new Dictionary<BloodTypeMarker, int>
            {
                {BloodTypeMarker.O, 0 },
                {BloodTypeMarker.ORh, 0 },
                {BloodTypeMarker.A, 0 },
                {BloodTypeMarker.ARh, 0 },
                {BloodTypeMarker.B, 0 },
                {BloodTypeMarker.BRh, 0 },
                {BloodTypeMarker.AB, 0 },
                {BloodTypeMarker.ABRh, 0 }
            };
        }


    }
}