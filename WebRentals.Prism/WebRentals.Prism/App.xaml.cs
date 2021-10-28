using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using WebRentals.Prism.Services;
using WebRentals.Prism.ViewModels;
using WebRentals.Prism.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace WebRentals.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("NTI2MTkzQDMxMzkyZTMzMmUzMGxsS2xDV1VVQ1ByQVRKZi9VR0xCWGdNdmZOTzBrc3NVc0JGRjVrcENJNGM9");

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/PropertiesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertiesPage, PropertiesPageViewModel>();
        }
    }
}
