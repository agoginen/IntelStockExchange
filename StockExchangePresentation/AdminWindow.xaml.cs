using StockExchangePresentation.StockExchangeServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockExchangePresentation
{
	/// <summary>
	/// Interaction logic for AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		public AdminWindow()
		{
			InitializeComponent();
        }

		//Event Handler To allow only integers
		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		//Event Handler to allow only decimals
		private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
			e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
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
