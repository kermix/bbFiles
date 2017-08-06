using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using GalaSoft.MvvmLight.Messaging;
using bbFiles.Messages;

namespace bbFiles.ViewModel
{
    public class StatisticsViewModel : ViewModelBase
    {
        IStatisticsDataAccessService _proxyService;
        SeriesCollection _seriesCollection;
        /// <summary>
        /// Gets or sets the series collection for stats.
        /// </summary>
        /// <value>
        /// The series collection.
        /// </value>
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set { _seriesCollection = value; RaisePropertyChanged("SeriesCollection"); }
        }
        /// <summary>
        /// Gets or sets the labels for stats chart.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public string[] Labels { get; set; }
        /// <summary>
        /// Gets or sets the formatter for serie's value.
        /// </summary>
        /// <value>
        /// The formatter.
        /// </value>
        public Func<int, string> Formatter { get; set; }
        /// <exclude />
        public RelayCommand RefreshCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsViewModel"/> class.
        /// </summary>
        /// <param name="proxyService">The data access service.</param>
        public StatisticsViewModel(IStatisticsDataAccessService proxyService)
        {
            _proxyService = proxyService;
            GetStatistics();
            Labels = new[] { "0", "A", "B", "AB" };
            Formatter = value => value.ToString("N") + "%";
            RefreshCommand = new RelayCommand(GetStatistics);
        }
        
        void GetStatistics()
        {
            try
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
            catch (NullReferenceException)
            {
            Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.DatabaseErrorTitle, Error = Resources.Strings.StatisticScaffoldError });
            }
            
        }
    }
}