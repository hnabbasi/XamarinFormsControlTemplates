using MovieNight.Data;
using MovieNight.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace MovieNight
{
    [AutoRegisterForNavigation]
    public partial class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"/NavigationPage/{nameof(MenuPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<IMovieService, MovieService>();
        }
    }
}
