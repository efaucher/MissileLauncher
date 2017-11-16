using MissileSharp;
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

namespace MissileLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CommandCenter _launcher;
        public MainWindow()
        {
            InitializeComponent();

            var launcherModel = LauncherModelFactory.GetLauncher("MissileSharp.ThunderMissileLauncher");
            _launcher = new CommandCenter(launcherModel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _launcher.Reset();
            /*launcher.Reset();       // reset to bottom left
            launcher.Up(300);       // move up 500 milliseconds
            launcher.Right(9000);   // turn right 1000 milliseconds
            launcher.Fire(2);*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _launcher.Right(1700);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _launcher.Left(1700);
        }
    }
}
