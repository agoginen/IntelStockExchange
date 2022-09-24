using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockExchangePresentation.View
{
	/// <summary>
	/// Interaction logic for AddStockView.xaml
	/// </summary>
	public partial class AddStockView : UserControl
	{
		public AddStockView()
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
	}
}
