
namespace Sale.ViewModels
{
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Sale.Helper;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {

        #region Atrributes
        private ObservableCollection<Product> products;
        private ApiService apiService;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { SetValue(ref this.products, value); }
        }
        public string Label { get; set; }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructor
        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
            instnace = this;
           
        }
        #endregion

        #region Singleton
        private static ProductsViewModel instnace;
        public static ProductsViewModel GetInstacne()
        {
            if(instnace == null)
            {
                return new ProductsViewModel();
            }
            return instnace;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
        #endregion


        #region Methods
        private async void LoadProducts()
        {

            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if(!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var url = Application.Current.Resources["UriAPI"].ToString();
            var prefix = Application.Current.Resources["UriAPrefix"].ToString();
            var controller = Application.Current.Resources["UriProductsController"].ToString();
            
            var response = await this.apiService.GetList<Product>(url, prefix, controller);
            if(!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            IsRefreshing = false;
        }
        #endregion


    }
}
