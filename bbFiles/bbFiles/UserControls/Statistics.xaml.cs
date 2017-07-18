using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace bbFiles.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();
            var stats = new Structs.Statistics();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Rh+",
                    Values = new ChartValues<int>
                    {
                        stats.BloodTypes["oWithMarker"],
                        stats.BloodTypes["aWithMarker"],
                        stats.BloodTypes["bWithMarker"],
                        stats.BloodTypes["abWithMarker"]
                    }
                },
                new ColumnSeries
                {
                    Title = "Rh-",
                    Values = new ChartValues<int>
                    {
                        stats.BloodTypes["oWithOutMarker"],
                        stats.BloodTypes["aWithOutMarker"],
                        stats.BloodTypes["bWithOutMarker"],
                        stats.BloodTypes["abWithOutMarker"]
                    }
                }
            };

            Labels = new[] { "0", "A", "B", "AB" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
