using MissileSharp;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MissileLauncher
{

    public partial class MainWindow : Window
    {
        private readonly ICommandCenter _launcher;
        private int _positionX = 0;
        private int _positionY = 0;

        public MainWindow()
        {
            InitializeComponent();

            var launcherModel = LauncherModelFactory.GetLauncher("MissileSharp.ThunderMissileLauncher");
            _launcher = new CommandCenter(launcherModel);
            _launcher.Reset();
        }

        private async void Hugo_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(1700, 0);
        }

        private async void Tim_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(5100, 0);
        }

        private async void An_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(3400, 0);
        }

        private async void Etienne_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(0, 0);
        }

        private async void Marc_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(700, 300);
        }

        private async void Eric_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(1100, 400);
        }

        private async void Vincent_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(750, 700);
        }

        private async void JeanLaurence_Click(object sender, RoutedEventArgs e)
        {
            await MoveLauncher(1100, 700);
        }

        private async void Fire_Click(object sender, RoutedEventArgs e)
        {
            await _launcher.Fire(1);
        }

        private async Task MoveLauncher(int positionX, int positionY)
        {
            int movementX = positionX - _positionX;
            int movementY = positionY - _positionY;

            _positionX += movementX;
            _positionY += movementY;

            if (movementX > 0) // Move right
            {
                await MoveRight(movementX);
            }
            else if (movementX < 0)// Move Left
            {
                movementX = Math.Abs(movementX);
                await MoveLeft(movementX);
            }

            if (movementY > 0)
            {
                await MoveUp(movementY);
            }
            else if (movementY < 0)
            {
                movementY = Math.Abs(movementY);
                await MoveDown(movementY);
            }
        }

        private async Task MoveRight(int movement)
        {
            await _launcher.Right(movement);
        }

        private async Task MoveLeft(int movement)
        {
            await _launcher.Left(movement);
        }

        private async Task MoveUp(int movement)
        {
            await _launcher.Up(movement);
        }

        private async Task MoveDown(int movement)
        {
            await _launcher.Down(movement);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _launcher.Reset();
            _positionX = 0;
            _positionY = 0;
        }
    }
}
