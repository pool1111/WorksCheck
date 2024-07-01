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
using System.Windows.Shapes;
using WorksCheck.Resources.Settings;
using WorksCheck.Resources.UserControls;
using WorksCheck.Scripts;

namespace WorksCheck.Views
{
    /// <summary>
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(""+UserSetting.Default.Plan.Count);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            UserSetting.Default.Plan = new List<HomeworkData>();
            UserSetting.Default.Save();
        }
    }
}
