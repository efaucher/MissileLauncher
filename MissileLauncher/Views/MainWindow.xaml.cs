using System.Windows;

namespace MissileLauncher
{
    public partial class MainWindow : Window
    {
        private readonly CommandCenterViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new CommandCenterViewModel();
        }

        private void Hugo_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(1700, 0);
        }

        private void Tim_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(6000, 0);
        }

        private void An_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(3400, 0);
        }

        private void Etienne_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(0, 0);
        }

        private void Marc_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(700, 300);
        }

        private void Eric_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(1100, 400);
        }

        private void Vincent_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(750, 700);
        }

        private void JeanLaurence_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveLauncher(1100, 700);
        }

        private void Fire_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Fire();
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Reset();
        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            Manual window = new Manual(_viewModel);
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
