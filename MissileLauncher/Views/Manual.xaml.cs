using System;
using System.Threading.Tasks;
using System.Windows;

namespace MissileLauncher
{
    /// <summary>
    /// Interaction logic for Manual.xaml
    /// </summary>
    public partial class Manual : Window
    {
        private readonly CommandCenterViewModel _viewModel;
        private bool _moving;
        public Manual(CommandCenterViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _moving = false;
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            Move(new Task(() => _viewModel.MoveUp()));
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            Move(new Task(() => _viewModel.MoveDown()));
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            Move(new Task(() => _viewModel.MoveLeft()));
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            Move(new Task(() => _viewModel.MoveRight()));
        }

        private async void Fire_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => { _viewModel.Fire(); });
        }

        private async void Move(Task action)
        {
            if (_moving)
            {
                _viewModel.Stop();
                _moving = false;
            }
            else
            {
                action.Start();
                _moving = true;
            }
        }
    }
}
