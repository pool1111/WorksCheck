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
using WorksCheck.Resources.Settings;
using WorksCheck.Scripts;
using WorksCheck.ViewModels;
using WorksCheck.Views;

namespace WorksCheck.Resources.UserControls
{
    /// <summary>
    /// HomeworkDataContainer.xaml の相互作用ロジック
    /// </summary>
    public partial class HomeworkDataContainer : UserControl
    {
        public HomeworkDataContainer()
        {
            InitializeComponent();
            HomeworkData a = new();
            a.Subject = SubjectData.CallG;
        }

        public static readonly DependencyProperty ShowHomeworkDataProperty = DependencyProperty.Register(
            "ShowHomeworkData",typeof(HomeworkData), typeof(HomeworkDataContainer), new PropertyMetadata(new HomeworkData(),new PropertyChangedCallback((s,e)=>
                (s as HomeworkDataContainer).OnShowHomeworkDataPropertyChanged(s,e)
            )));

        public static readonly DependencyProperty DayRackProperty = DependencyProperty.Register(
    "DayRack", typeof(DayRack), typeof(HomeworkDataContainer), new PropertyMetadata(null, null));

        public HomeworkData ShowHomeworkData
        {
            get { return (HomeworkData)GetValue(ShowHomeworkDataProperty); }
            set {  SetValue(ShowHomeworkDataProperty, value);}
        }

        public DayRack DayRack
        {
            get { return (DayRack)GetValue(DayRackProperty); }
            set { SetValue(DayRackProperty, value);}
        }

        private void OnShowHomeworkDataPropertyChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            HomeworkData data = (HomeworkData)e.NewValue;

            switch(data.Priority)
            {
                case Priority.高:
                    Priorityimg.Tag = "高";
                    break;
                case Priority.中:
                    Priorityimg.Tag = "中";
                    break;
                case Priority.低:
                    Priorityimg.Tag = "低";
                    break;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            UserSetting.Default.Plan.Remove(ShowHomeworkData);
            UserSetting.Default.Save();
            DayRack.LoadHomeworkData();
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            var SHViewModel = new SetHomeworkViewModel(ShowHomeworkData);
            SHViewModel.DayRack = DayRack;
            var SHWindow = new SetHomeworkWindow(SHViewModel);
            SHViewModel.View = SHWindow;
            SHWindow.ShowDialog();
        }
    }
}
