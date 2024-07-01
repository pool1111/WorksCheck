using System.Configuration;
using System.Data;
using System.Windows;
using WorksCheck.Resources.Settings;
using WorksCheck.Scripts;

namespace WorksCheck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(UserSetting.Default.Plan == null)
            {
                UserSetting.Default.Plan = new List<HomeworkData>();
                UserSetting.Default.Save();
            }
        }
    }

}
