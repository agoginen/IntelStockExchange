using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;

namespace StockExchangePresentation
{
	public class ViewModelMessage : MessageBase
    {
        public static string Message_Navigate = "Navigate";
        public static string Message_LoadOrderSummary = "LoadOrderSummary";
        public static string Message_AddToCart = "AddtoCart";
        public static string Navigation_ProceedOrder = "ProceedOrder";
        public static string Message_RemoveFromCart = "RemoveFromCart";
        public static string Message_OpenDialog = "OpenDialog";
        public static string Dialog_SelectQuantityAndSize = "SelectQuantityAndSize";
        public static string Dialog_OrderSuccess = "OrderSuccess";
        public static string Dialog_OrderFailed = "OrderFailed";
        public static string Message_ClearCart = "ClearCart";
        public static string Navigation_Login = "Login";
        public static string Dialog_LoginFailed = "LoginFailed";
        public static string Dialog_RegistrationFailed = "RegistrationFailed";
        public static string Dialog_RegistrationSuccess = "RegistrationSuccess";
        public int Id{ get; set; }
        public string Message { get; set; }
        public string NavigateTo { get; set; }
        public string Dialog { get; set; }
    }
}
