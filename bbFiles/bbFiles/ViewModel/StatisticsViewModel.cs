using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using LiveCharts;
using LiveCharts.Wpf;
using System;

namespace bbFiles.ViewModel
{
    public class StatisticsViewModel : ViewModelBase
    {
        IStatisticsDataAccessService _proxyService;
        SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set { _seriesCollection = value; RaisePropertyChanged("SeriesCollection"); }
        }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        public StatisticsViewModel(IStatisticsDataAccessService proxyService)
        {
            _proxyService = proxyService;
            GetStatistics();
            Labels = new[] { "0", "A", "B", "AB" };
            Formatter = value => value.ToString("N");
            RefreshCommand = new RelayCommand(GetStatistics);
        }
        
        void GetStatistics()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Rh+",
                    Values = new ChartValues<long>
                    {
                        _proxyService.GetStatistic(BloodTypeMarker.ORh),
                        _proxyService.GetStatistic(BloodTypeMarker.ARh),
                        _proxyService.GetStatistic(BloodTypeMarker.BRh),
                        _proxyService.GetStatistic(BloodTypeMarker.ABRh)
                    }
                },
                new ColumnSeries
                {
                    Title = "Rh-",
                    Values = new ChartValues<long>
                    {
                        _proxyService.GetStatistic(BloodTypeMarker.O),
                        _proxyService.GetStatistic(BloodTypeMarker.A),
                        _proxyService.GetStatistic(BloodTypeMarker.B),
                        _proxyService.GetStatistic(BloodTypeMarker.AB)
                    }
                }
            };
            RaisePropertyChanged("SeriesCollection");
        }




    }
}