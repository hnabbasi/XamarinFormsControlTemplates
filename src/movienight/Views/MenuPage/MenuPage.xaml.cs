using Xamarin.Forms;
using Xamarin.Essentials;
using System;

namespace MovieNight.Views.MenuPage
{
    public partial class MenuPage : ContentPage
    {
        private readonly ControlTemplate _horizontal = new HorizontalMenuTemplate();
        private readonly ControlTemplate _vertical = new VerticalMenuTemplate();

        public MenuPage()
        {
            InitializeComponent();
            DeviceDisplay_MainDisplayInfoChanged(this, new DisplayInfoChangedEventArgs(DeviceDisplay.MainDisplayInfo));
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            ControlTemplate = e.DisplayInfo.Orientation == DisplayOrientation.Portrait
                ? _vertical
                : _horizontal;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }
    }
}
