using MissileSharp;
using System.Windows;

namespace MissileLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICommandCenter _launcher;
        public MainWindow()
        {
            InitializeComponent();

            var launcherModel = LauncherModelFactory.GetLauncher("MissileSharp.ThunderMissileLauncher");
            _launcher = new CommandCenter(launcherModel);
        }

        private async void Hugo_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
            await _launcher.Right(1700);
        }

        private async void Tim_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
            await _launcher.Right(1700 * 3);
        }

        private async void An_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
            await _launcher.Right(1700 * 2);
        }

        private async void Etienne_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
        }

        private async void Marc_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
            await _launcher.Right(700);
            await _launcher.Up(200);
        }

        private async void Eric_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
            await _launcher.Right(1100);
            await _launcher.Up(400);
        }

        private async void Vincent_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
        }

        private async void JeanLaurence_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Reset();
        }

        private async void Fire_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Fire(1);
        }
    }
}
