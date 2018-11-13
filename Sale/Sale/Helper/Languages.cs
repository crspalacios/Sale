namespace Sale.Helper
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resources.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resources.Accept; }
        }

        public static string Error
        {
            get { return Resources.Error; }
        }

        public static string Products
        {
            get { return Resources.Products; }
        }

        public static string NoInternet
        {
            get { return Resources.NoInternet; }
        }

        public static string TurnOnInternt
        {
            get { return Resources.TurnOnInternt; }
        }

        public static string AddProduct
        {
            get { return Resources.AddProduct; }
        }

        public static string Description
        {
            get { return Resources.Description; }
        }

        public static string DescriptionPlaceholder
        {
            get { return Resources.DescriptionPlaceholder; }
        }

        public static string Price
        {
            get { return Resources.Price; }
        }

        public static string PricePlaceholder
        {
            get { return Resources.PricePlaceholder; }
        }

        public static string Remarks
        {
            get { return Resources.Remarks; }
        }

        public static string Save
        {
            get { return Resources.Save; }
        }

        public static string ChangeImage
        {
            get { return Resources.ChangeImage; }
        }

        public static string DescriptionError
        {
            get { return Resources.DescriptionError; }
        }

        public static string PriceError
        {
            get { return Resources.PriceError; }
        }

        public static string ImageSource
        {
            get { return Resources.ImageSource; }
        }

        public static string FromGallery
        {
            get { return Resources.FromGallery; }
        }

        public static string NewPicture
        {
            get { return Resources.NewPicture; }
        }

        public static string Cancel
        {
            get { return Resources.Cancel; }
        }

        public static string DeleteConfirmation
        {
            get { return Resources.DeleteConfirmation; }
        }


        public static string Edit
        {
            get { return Resources.Edit; }
        }


        public static string Yes
        {
            get { return Resources.Yes; }
        }




        public static string No
        {
            get { return Resources.No; }
        }




    }

}
