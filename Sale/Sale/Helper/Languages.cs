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
    }

}
