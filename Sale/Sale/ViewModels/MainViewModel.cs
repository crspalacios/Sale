
namespace Sale.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sale.Views;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class MainViewModel
    {
        #region Properties
        public ProductsViewModel Products { get; set; }
        public AddProductViewModel AddProduct { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }

        #endregion
        #region Commands
        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProdut);
            }

        }
        #endregion
        #region Methods
        private void GoToAddProdut()
        {
            this.AddProduct = new AddProductViewModel();
            Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        } 
        #endregion
    }
}
