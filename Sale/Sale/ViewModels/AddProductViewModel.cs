

namespace Sale.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Sale.Common.Models;
    using Sale.Helper;
    using Sale.Services;
    using Sale.Views;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddProductViewModel : BaseViewModel
    {
        #region Atributes
        private bool isRunning;
        private bool isEnabled;
        private ApiService apiService;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties
        public string Description {get; set; }
        public string Price { get; set; }
        public string Remarks { get; set; }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region Constructors
        public AddProductViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.imageSource = "noproduct2";
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        #endregion

        #region Methods
        #region Capturar Foto
            private async void ChangeImage()
            {
                {
                    await CrossMedia.Current.Initialize();

                    var source = await Application.Current.MainPage.DisplayActionSheet(
                        Languages.ImageSource,
                        Languages.Cancel,
                        null,
                        Languages.FromGallery,
                        Languages.NewPicture);

                    if (source == Languages.Cancel)
                    {
                        this.file = null;
                        return;
                    }

                    if (source == Languages.NewPicture)
                    {
                        this.file = await CrossMedia.Current.TakePhotoAsync(
                            new StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg",
                                PhotoSize = PhotoSize.Small,
                            }
                        );
                    }
                    else
                    {
                        this.file = await CrossMedia.Current.PickPhotoAsync();
                    }

                    if (this.file != null)
                    {
                        this.ImageSource = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                    }

                } 
            #endregion
        }

        private async void Save()
        {

            #region Local Validations
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                                Languages.DescriptionError,
                                                                Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                                Languages.PriceError,
                                                                Languages.Accept);
                return;
            }

            var price = decimal.Parse(this.Price);
            if (price < 0)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                                Languages.PriceError,
                                                                Languages.Accept);
                return;
            }
            #endregion

            #region Validations Get Photo
            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            } 
            #endregion

            this.IsRunning = true;
            this.IsEnabled = false;

            #region Check Conecctions
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.isRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            } 
            #endregion

            var product = new Product
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks,
                ImageArray = imageArray,
            };

            var url = Application.Current.Resources["UriAPI"].ToString();
            var prefix = Application.Current.Resources["UriAPrefix"].ToString();
            var controller = Application.Current.Resources["UriProductsController"].ToString();

            var response = await this.apiService.Post(url, prefix, controller, product);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var newProduct = (Product)response.Result;
            var viewModel = ProductsViewModel.GetInstacne();
            viewModel.Products.Add(newProduct);
            //viewModel.Products = viewModel.Products.OrderBy(p => p.Description).ToList();
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        #endregion
    }
}
