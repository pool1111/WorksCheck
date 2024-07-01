using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
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

using WorksCheck.Resources.Settings;
using WorksCheck.Scripts;

namespace WorksCheck.Resources.UserControls
{
    /// <summary>
    /// DayRack.xaml の相互作用ロジック
    /// </summary>
    public partial class DayRack : UserControl
    {
        public DayRack()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(
            "Date", typeof(DateTime), typeof(DayRack), new PropertyMetadata(new DateTime(1111, 1, 1),new PropertyChangedCallback((s, e) =>
            {
                (s as DayRack).OnDatePropertyChanged(s, e);
            }
                )));

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        private void OnDatePropertyChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            DateTime date = (DateTime)e.NewValue;

            DayText.Text = date.Month+"月"+date.Day + "日";
            DateText.Text = date.ToString("dddd");
            if (date.DayOfWeek == DayOfWeek.Sunday)
                DateText.Foreground = FindResource("SundayColor") as SolidColorBrush;
            else if (date.DayOfWeek == DayOfWeek.Saturday)
                DateText.Foreground = FindResource("SaturdayColor") as SolidColorBrush;
            else
                DateText.Foreground = FindResource("TextColor") as SolidColorBrush;

            if(date.Date == DateTime.Today)
            {
                TodayText.Visibility = Visibility.Visible;
            }
            else
            {
                TodayText.Visibility = Visibility.Hidden;
            }
            
            LoadHomeworkData();
        }

        private List<HomeworkData> ThisHomeworkData = new();

        public void LoadHomeworkData()
        {
            HomeworkDataPanel.Children.Clear();

            ThisHomeworkData = new List<HomeworkData>();
            foreach (var data in UserSetting.Default.Plan)
            {
                if(data.Date == Date && data.Priority == Priority.高)
                {
                    ThisHomeworkData.Add(data);
                }
            }
            foreach (var data in UserSetting.Default.Plan)
            {
                if(data.Date == Date && data.Priority == Priority.中)
                {
                    ThisHomeworkData.Add(data);
                }
            }           
            foreach (var data in UserSetting.Default.Plan)
            {
                if(data.Date == Date && data.Priority == Priority.低)
                {
                    ThisHomeworkData.Add(data);
                }
            }
            foreach(var data in ThisHomeworkData)
            {
                HomeworkDataContainer container = new();
                container.ShowHomeworkData = data;
                container.DayRack = this;

                Grid grid = new ();
                grid.Height = 40;
                grid.Margin = new Thickness(10, 5, 5, 10);
                grid.Children.Add(container);

                HomeworkDataPanel.Children.Add(grid);
            }
            SumData.Text = "" + ThisHomeworkData.Count;
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ThisHomeworkData:" + ThisHomeworkData.Count);
        }
    }
}
