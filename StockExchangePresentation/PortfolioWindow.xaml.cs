using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation
{
	/// <summary>
	/// Interaction logic for PortfolioWindow.xaml
	/// </summary>
	public partial class PortfolioWindow : Window
	{
		public PortfolioWindow()
		{
			InitializeComponent();
		}

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }
    }
}
