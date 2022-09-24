﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockExchangePresentation.StockExchangeServices {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/SE_Entities")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.Balance> BalancesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateTimeAddedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeDeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeUpdatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> UserStocksField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.Balance> Balances {
            get {
                return this.BalancesField;
            }
            set {
                if ((object.ReferenceEquals(this.BalancesField, value) != true)) {
                    this.BalancesField = value;
                    this.RaisePropertyChanged("Balances");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTimeAdded {
            get {
                return this.DateTimeAddedField;
            }
            set {
                if ((this.DateTimeAddedField.Equals(value) != true)) {
                    this.DateTimeAddedField = value;
                    this.RaisePropertyChanged("DateTimeAdded");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeDeleted {
            get {
                return this.DateTimeDeletedField;
            }
            set {
                if ((this.DateTimeDeletedField.Equals(value) != true)) {
                    this.DateTimeDeletedField = value;
                    this.RaisePropertyChanged("DateTimeDeleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeUpdated {
            get {
                return this.DateTimeUpdatedField;
            }
            set {
                if ((this.DateTimeUpdatedField.Equals(value) != true)) {
                    this.DateTimeUpdatedField = value;
                    this.RaisePropertyChanged("DateTimeUpdated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmailAddress {
            get {
                return this.EmailAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailAddressField, value) != true)) {
                    this.EmailAddressField = value;
                    this.RaisePropertyChanged("EmailAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> UserStocks {
            get {
                return this.UserStocksField;
            }
            set {
                if ((object.ReferenceEquals(this.UserStocksField, value) != true)) {
                    this.UserStocksField = value;
                    this.RaisePropertyChanged("UserStocks");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserType {
            get {
                return this.UserTypeField;
            }
            set {
                if ((this.UserTypeField.Equals(value) != true)) {
                    this.UserTypeField = value;
                    this.RaisePropertyChanged("UserType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Balance", Namespace="http://schemas.datacontract.org/2004/07/SE_Entities")]
    [System.SerializableAttribute()]
    public partial class Balance : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Balance1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateTimeAddedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeDeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeUpdatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private StockExchangePresentation.StockExchangeServices.User UserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> UserIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Balance1 {
            get {
                return this.Balance1Field;
            }
            set {
                if ((this.Balance1Field.Equals(value) != true)) {
                    this.Balance1Field = value;
                    this.RaisePropertyChanged("Balance1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTimeAdded {
            get {
                return this.DateTimeAddedField;
            }
            set {
                if ((this.DateTimeAddedField.Equals(value) != true)) {
                    this.DateTimeAddedField = value;
                    this.RaisePropertyChanged("DateTimeAdded");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeDeleted {
            get {
                return this.DateTimeDeletedField;
            }
            set {
                if ((this.DateTimeDeletedField.Equals(value) != true)) {
                    this.DateTimeDeletedField = value;
                    this.RaisePropertyChanged("DateTimeDeleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeUpdated {
            get {
                return this.DateTimeUpdatedField;
            }
            set {
                if ((this.DateTimeUpdatedField.Equals(value) != true)) {
                    this.DateTimeUpdatedField = value;
                    this.RaisePropertyChanged("DateTimeUpdated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public StockExchangePresentation.StockExchangeServices.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserStock", Namespace="http://schemas.datacontract.org/2004/07/SE_Entities")]
    [System.SerializableAttribute()]
    public partial class UserStock : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateTimeAddedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeDeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeUpdatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private StockExchangePresentation.StockExchangeServices.Stock StockField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StockCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StockIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private StockExchangePresentation.StockExchangeServices.User UserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTimeAdded {
            get {
                return this.DateTimeAddedField;
            }
            set {
                if ((this.DateTimeAddedField.Equals(value) != true)) {
                    this.DateTimeAddedField = value;
                    this.RaisePropertyChanged("DateTimeAdded");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeDeleted {
            get {
                return this.DateTimeDeletedField;
            }
            set {
                if ((this.DateTimeDeletedField.Equals(value) != true)) {
                    this.DateTimeDeletedField = value;
                    this.RaisePropertyChanged("DateTimeDeleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeUpdated {
            get {
                return this.DateTimeUpdatedField;
            }
            set {
                if ((this.DateTimeUpdatedField.Equals(value) != true)) {
                    this.DateTimeUpdatedField = value;
                    this.RaisePropertyChanged("DateTimeUpdated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public StockExchangePresentation.StockExchangeServices.Stock Stock {
            get {
                return this.StockField;
            }
            set {
                if ((object.ReferenceEquals(this.StockField, value) != true)) {
                    this.StockField = value;
                    this.RaisePropertyChanged("Stock");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StockCount {
            get {
                return this.StockCountField;
            }
            set {
                if ((this.StockCountField.Equals(value) != true)) {
                    this.StockCountField = value;
                    this.RaisePropertyChanged("StockCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StockId {
            get {
                return this.StockIdField;
            }
            set {
                if ((this.StockIdField.Equals(value) != true)) {
                    this.StockIdField = value;
                    this.RaisePropertyChanged("StockId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public StockExchangePresentation.StockExchangeServices.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Stock", Namespace="http://schemas.datacontract.org/2004/07/SE_Entities")]
    [System.SerializableAttribute()]
    public partial class Stock : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateTimeAddedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeDeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeUpdatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> HighPriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> LowPriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StockNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> UserStocksField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTimeAdded {
            get {
                return this.DateTimeAddedField;
            }
            set {
                if ((this.DateTimeAddedField.Equals(value) != true)) {
                    this.DateTimeAddedField = value;
                    this.RaisePropertyChanged("DateTimeAdded");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeDeleted {
            get {
                return this.DateTimeDeletedField;
            }
            set {
                if ((this.DateTimeDeletedField.Equals(value) != true)) {
                    this.DateTimeDeletedField = value;
                    this.RaisePropertyChanged("DateTimeDeleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeUpdated {
            get {
                return this.DateTimeUpdatedField;
            }
            set {
                if ((this.DateTimeUpdatedField.Equals(value) != true)) {
                    this.DateTimeUpdatedField = value;
                    this.RaisePropertyChanged("DateTimeUpdated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> HighPrice {
            get {
                return this.HighPriceField;
            }
            set {
                if ((this.HighPriceField.Equals(value) != true)) {
                    this.HighPriceField = value;
                    this.RaisePropertyChanged("HighPrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> LowPrice {
            get {
                return this.LowPriceField;
            }
            set {
                if ((this.LowPriceField.Equals(value) != true)) {
                    this.LowPriceField = value;
                    this.RaisePropertyChanged("LowPrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StockName {
            get {
                return this.StockNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StockNameField, value) != true)) {
                    this.StockNameField = value;
                    this.RaisePropertyChanged("StockName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> UserStocks {
            get {
                return this.UserStocksField;
            }
            set {
                if ((object.ReferenceEquals(this.UserStocksField, value) != true)) {
                    this.UserStocksField = value;
                    this.RaisePropertyChanged("UserStocks");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StockExchangeServices.IStockExchangeOrder")]
    public interface IStockExchangeOrder {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/Register", ReplyAction="http://tempuri.org/IStockExchangeOrder/RegisterResponse")]
        bool Register(string userName, string password, string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/Register", ReplyAction="http://tempuri.org/IStockExchangeOrder/RegisterResponse")]
        System.Threading.Tasks.Task<bool> RegisterAsync(string userName, string password, string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/Login", ReplyAction="http://tempuri.org/IStockExchangeOrder/LoginResponse")]
        SE_Services.ViewModels.UserViewModel Login(StockExchangePresentation.StockExchangeServices.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/Login", ReplyAction="http://tempuri.org/IStockExchangeOrder/LoginResponse")]
        System.Threading.Tasks.Task<SE_Services.ViewModels.UserViewModel> LoginAsync(StockExchangePresentation.StockExchangeServices.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/Logout", ReplyAction="http://tempuri.org/IStockExchangeOrder/LogoutResponse")]
        bool Logout(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/Logout", ReplyAction="http://tempuri.org/IStockExchangeOrder/LogoutResponse")]
        System.Threading.Tasks.Task<bool> LogoutAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/GetAllStocks", ReplyAction="http://tempuri.org/IStockExchangeOrder/GetAllStocksResponse")]
        System.Collections.Generic.List<SE_Services.ViewModels.StockViewModel> GetAllStocks(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/GetAllStocks", ReplyAction="http://tempuri.org/IStockExchangeOrder/GetAllStocksResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<SE_Services.ViewModels.StockViewModel>> GetAllStocksAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/GetAllUserStocks", ReplyAction="http://tempuri.org/IStockExchangeOrder/GetAllUserStocksResponse")]
        System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> GetAllUserStocks(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/GetAllUserStocks", ReplyAction="http://tempuri.org/IStockExchangeOrder/GetAllUserStocksResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock>> GetAllUserStocksAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/GetStockPrice", ReplyAction="http://tempuri.org/IStockExchangeOrder/GetStockPriceResponse")]
        int GetStockPrice(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/GetStockPrice", ReplyAction="http://tempuri.org/IStockExchangeOrder/GetStockPriceResponse")]
        System.Threading.Tasks.Task<int> GetStockPriceAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/ProceedOrder", ReplyAction="http://tempuri.org/IStockExchangeOrder/ProceedOrderResponse")]
        bool ProceedOrder(int userId, System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> userStocks);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockExchangeOrder/ProceedOrder", ReplyAction="http://tempuri.org/IStockExchangeOrder/ProceedOrderResponse")]
        System.Threading.Tasks.Task<bool> ProceedOrderAsync(int userId, System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> userStocks);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStockExchangeOrderChannel : StockExchangePresentation.StockExchangeServices.IStockExchangeOrder, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockExchangeOrderClient : System.ServiceModel.ClientBase<StockExchangePresentation.StockExchangeServices.IStockExchangeOrder>, StockExchangePresentation.StockExchangeServices.IStockExchangeOrder {
        
        public StockExchangeOrderClient() {
        }
        
        public StockExchangeOrderClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StockExchangeOrderClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockExchangeOrderClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockExchangeOrderClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Register(string userName, string password, string emailAddress) {
            return base.Channel.Register(userName, password, emailAddress);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterAsync(string userName, string password, string emailAddress) {
            return base.Channel.RegisterAsync(userName, password, emailAddress);
        }
        
        public SE_Services.ViewModels.UserViewModel Login(StockExchangePresentation.StockExchangeServices.User user) {
            return base.Channel.Login(user);
        }
        
        public System.Threading.Tasks.Task<SE_Services.ViewModels.UserViewModel> LoginAsync(StockExchangePresentation.StockExchangeServices.User user) {
            return base.Channel.LoginAsync(user);
        }
        
        public bool Logout(int userId) {
            return base.Channel.Logout(userId);
        }
        
        public System.Threading.Tasks.Task<bool> LogoutAsync(int userId) {
            return base.Channel.LogoutAsync(userId);
        }
        
        public System.Collections.Generic.List<SE_Services.ViewModels.StockViewModel> GetAllStocks(int userId) {
            return base.Channel.GetAllStocks(userId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<SE_Services.ViewModels.StockViewModel>> GetAllStocksAsync(int userId) {
            return base.Channel.GetAllStocksAsync(userId);
        }
        
        public System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> GetAllUserStocks(int userId) {
            return base.Channel.GetAllUserStocks(userId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock>> GetAllUserStocksAsync(int userId) {
            return base.Channel.GetAllUserStocksAsync(userId);
        }
        
        public int GetStockPrice(int id) {
            return base.Channel.GetStockPrice(id);
        }
        
        public System.Threading.Tasks.Task<int> GetStockPriceAsync(int id) {
            return base.Channel.GetStockPriceAsync(id);
        }
        
        public bool ProceedOrder(int userId, System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> userStocks) {
            return base.Channel.ProceedOrder(userId, userStocks);
        }
        
        public System.Threading.Tasks.Task<bool> ProceedOrderAsync(int userId, System.Collections.Generic.List<StockExchangePresentation.StockExchangeServices.UserStock> userStocks) {
            return base.Channel.ProceedOrderAsync(userId, userStocks);
        }
    }
}
