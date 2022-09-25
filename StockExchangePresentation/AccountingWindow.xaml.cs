using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation
{
	/// <summary>
	/// Interaction logic for AccountingWindow.xaml
	/// </summary>
	public partial class AccountingWindow : Window
	{
		public AccountingWindow()
		{
			InitializeComponent();
		}

        //Event Handler To allow only integers
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }
    }
}
