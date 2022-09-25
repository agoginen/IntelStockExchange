using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using StockExchangePresentation.StockExchangeServices;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class LoginViewModel : ViewModelBase
    {
        private User user;
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public string UserName
        {
            get
            {
                return user.UserName;
            }
            set
            {
                user.UserName = value;
            }
        }

        public string Password
        {
            get
            {
                return user.Password;
            }
            set
            {
                user.Password = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return user.EmailAddress;
            }
            set
            {
                user.EmailAddress = value;
            }
        }

        public LoginViewModel()
        {
            this.user = new User();
            LoginCommand = new RelayCommand(LoginUser);
            RegisterCommand = new RelayCommand(RegisterUser);
        }

        /// <summary>
        /// Calls Register Service and registers users
        /// </summary>
        private async void RegisterUser()
        {
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            var success = await client.RegisterAsync(UserName, Password, EmailAddress);
            client.Close();

            if (success)
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Registering the " + UserName + " has failed. Please contact administrator if problem persists.","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Calls Login Service and logins the user
        /// </summary>
        private async void LoginUser()
        {
            StockExchangeOrderClient client = new StockExchangeOrderClient();
			var loggedInUser = await client.LoginAsync(user);
			client.Close();

			// If login succeeded and we got a valid Id
			if (loggedInUser.Id > 0)
			{
                //If Regular User
                if (loggedInUser.UserType == 1)
				{
                    //Open UserHomeWindow
                    UserHomeWindow userWindow = new UserHomeWindow();
                    userWindow.Show();
                    //Close MainWindow
                    Application.Current.Windows[0].Close();
                }
                //If Admin User
                else if(loggedInUser.UserType == 2)
				{
                    //Open AdminWindow
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    //Close MainWindow
                    Application.Current.Windows[0].Close();
				}
				else
				{
                    MessageBox.Show(UserName + "login was  . Please Register the user first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
			else
			{
                MessageBox.Show("Logging in the " + UserName + " has failed. Please Register the user first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}