
namespace Sale.ViewModels
{
    using Common.Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {

        #region Atrributes
        private ObservableCollection<Product> products;
        private ApiService apiService;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { SetValue(ref this.products, value); }
        }
        public string Label { get; set; }
        #endregion

        #region Constructor
        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
            Label="bindado"
        }
        #endregion

        #region Methods

        private async void LoadProducts()
        {
            var response = await this.apiService.GetList<Product>("https://saleapi.azurewebsites.net",
                                                                  "/api",
                                                                  "/Products");
            if(!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }
        #endregion
    }
}
