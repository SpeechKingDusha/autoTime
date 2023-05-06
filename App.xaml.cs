using autoTime.Models;
using System.Windows;

namespace autoTime
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        ConfigAPP configAPP = ConfigAPP.initialize();
    }
}
