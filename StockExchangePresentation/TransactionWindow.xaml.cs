﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockExchangePresentation
{
	/// <summary>
	/// Interaction logic for TransactionWindow.xaml
	/// </summary>
	public partial class TransactionWindow : INotifyPropertyChanged
	{
        private int _stockId { get; set; }
        private decimal _stockPrice { get; set; }
        private string _stockName { get; set; }
        private int _stockCount { get; set; }
        private string _orderType { get; set; }
        private string _transaction { get; set; }

        public string StockName
        {
            get
            {
                return _stockName;
            }
            set
            {
                if(_stockName != value)
				{
                    _stockName = value;
                    OnPropertyChanged();
                }
            }
        }

		public decimal StockPrice
        {
            get
            {
                return _stockPrice;
            }
            set
            {
                if (_stockPrice != value)
                {
                    _stockPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StockCount
        {
            get
            {
                return _stockCount;
            }
            set
            {
                if (_stockCount != value)
                {
                    _stockCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                if (_orderType != value)
                {
                    _orderType = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Transaction
        {
            get
            {
                return _transaction;
            }
            set
            {
                if (_transaction != value)
                {
                    _transaction = value;
                    OnPropertyChanged();
                }
            }
        }

        public TransactionWindow(int stockId, string stockName, decimal stockPrice, string transaction)
		{
            DataContext = this;
            _stockId = stockId;
            _stockName = stockName;
            _stockPrice = stockPrice;
            _transaction = transaction;
            InitializeComponent();
        }

		public event PropertyChangedEventHandler PropertyChanged;


        private void Transaction_Click(object sender, RoutedEventArgs e)
		{

		}

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
			Application.Current.Windows[1].Close();
		}

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }
    }
}