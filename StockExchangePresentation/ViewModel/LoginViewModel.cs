using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private User user;
        private int userId;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string LoginLabel { get; set; }
        public string UserName
        {
            get
            {
                return user.UserName;
            }
            set
            {
                user.UserName = value;
                RaisePropertyChanged("UserName");
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
                RaisePropertyChanged("Password");
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
                RaisePropertyChanged("EmailAddress");
            }
        }

        public LoginViewModel()
        {
            this.user = new User();

            LoginCommand = new RelayCommand(LoginUser, () => { return LoginLabel == "Login"; });
            RegisterCommand = new RelayCommand(RegisterUser);

            LoginLabel = "Login";
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
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_RegistrationSuccess
                });

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_RegistrationFailed
                });

                MessageBox.Show("Registering the " + UserName + " has failed. Please contact administrator if problem persists.","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Calls Login Service and logins the user
        /// </summary>
        private async void LoginUser()
        {
			LoginLabel = "Logging in...";
			RaisePropertyChanged("LoginLabel");
			((RelayCommand)LoginCommand).RaiseCanExecuteChanged();

            StockExchangeOrderClient client = new StockExchangeOrderClient();
			var loginId = await client.LoginAsync(user);
			userId = loginId;
			client.Close();

			LoginLabel = "Login";
			RaisePropertyChanged("LoginLabel");
			((RelayCommand)LoginCommand).RaiseCanExecuteChanged();

			// if login succeeded and we got a valid Id
			if (userId > 0)
			{
				Messenger.Default.Send(new ViewModelMessage
				{
					Id = userId,
					Message = ViewModelMessage.Message_Navigate,
					NavigateTo = ViewModelMessage.Navigation_ProceedOrder
				});

                //Open UserHomeWindow
                UserHomeWindow userWindow = new UserHomeWindow(userId);
                userWindow.Show();
                //Close MainWindow
                Application.Current.Windows[0].Close();
            }
			else
			{
				Messenger.Default.Send(new ViewModelMessage
				{
					Message = ViewModelMessage.Message_OpenDialog,
					Dialog = ViewModelMessage.Dialog_LoginFailed
				});

                MessageBox.Show("Logging in the " + UserName + " has failed. Please Register the user first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}