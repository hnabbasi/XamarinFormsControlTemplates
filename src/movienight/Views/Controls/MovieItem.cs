using Xamarin.Forms;
using Xamarin.Essentials;
using System;

namespace MovieNight.Views.Controls
{
    public class MovieItem : ContentView, IDisposable
    {
        #region Control Templates
        readonly ControlTemplate _verticalTemplate = new ControlTemplate(typeof(VerticalMovieTemplate));
        readonly ControlTemplate _horizontalTemplate = new ControlTemplate(typeof(HorizontalMovieTemplate));
        #endregion

        public MovieItem()
        {
            Padding = new Thickness(8);
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            DeviceDisplay_MainDisplayInfoChanged(this, new DisplayInfoChangedEventArgs(DeviceDisplay.MainDisplayInfo));
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e) {
            ControlTemplate = e.DisplayInfo.Orientation == DisplayOrientation.Portrait ? _verticalTemplate : _horizontalTemplate;
        }

        #region Bindable Properties
        public static BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(MovieItem));

        public static BindableProperty YearProperty = BindableProperty.Create(nameof(Year),
            typeof(string),
            typeof(MovieItem));

        public static BindableProperty AvatarProperty = BindableProperty.Create(nameof(Avatar),
            typeof(ImageSource),
            typeof(MovieItem));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Year
        {
            get { return (string)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        public ImageSource Avatar
        {
            get { return (ImageSource)GetValue(AvatarProperty); }
            set { SetValue(AvatarProperty, value); }
        }
        #endregion

        public void Dispose()
        {
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }
    }

    public class VerticalMovieTemplate : ContentView
    {
        public VerticalMovieTemplate()
        {
            var titleLabel = new Label {
                Style = (Style)Application.Current.Resources["MovieName"]
            };
            titleLabel.SetBinding(Label.TextProperty, new TemplateBinding("Title"));

            var icon = new Image() {
                Aspect = Aspect.AspectFill
            };
            icon.SetBinding(Image.SourceProperty, new TemplateBinding("Avatar"));

            var nameStack = new StackLayout
            {
                Padding = new Thickness(5.0),
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = (Color)Application.Current.Resources["PrimaryTranslucent"],
                HeightRequest = 50.0,
                Children =
                {
                    titleLabel
                }
            };

            var iconsStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = (Color)Application.Current.Resources["Accent"],
                Padding = new Thickness(5.0),
                Children =
                {
                    new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = "https://img.icons8.com/ios-glyphs/48/hearts.png",
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    },
                    new Label {
                        Text = "|",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.Silver
                    },
                    new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = "https://img.icons8.com/ios-glyphs/48/share.png",
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    },
                }
            };

            var grid = new Grid() {
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(40) }
                }
            };

            grid.Children.Add(icon, 0, 0);
            grid.Children.Add(nameStack, 0, 0);
            grid.Children.Add(iconsStack, 0, 1);

            Content = grid;
        }
    }

    public class HorizontalMovieTemplate : ContentView
    {
        public HorizontalMovieTemplate()
        {
            var titleLabel = new Label
            {
                Style = (Style)Application.Current.Resources["MovieName"]
            };
            titleLabel.SetBinding(Label.TextProperty, new TemplateBinding("Title"));

            var yearLabel = new Label
            {
                Style = (Style)Application.Current.Resources["MovieYear"]
            };
            yearLabel.SetBinding(Label.TextProperty, new TemplateBinding("Year"));

            var icon = new Image()
            {
                Aspect = Aspect.AspectFill,
                WidthRequest = 80.0,
                HeightRequest = 80.0
            };
            icon.SetBinding(Image.SourceProperty, new TemplateBinding("Avatar"));

            var nameStack = new StackLayout
            {
                Padding = new Thickness(5.0),
                BackgroundColor = (Color)Application.Current.Resources["Primary"],
                HeightRequest = 50.0,
                Children =
                {
                    titleLabel,
                    yearLabel
                }
            };

            var iconsStack = new StackLayout
            {
                BackgroundColor = (Color)Application.Current.Resources["Accent"],
                Padding = new Thickness(5.0),
                Children =
                {
                    new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = "https://img.icons8.com/ios-glyphs/48/hearts.png",
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    },
                    new Label {
                        Text = "-",
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Silver
                    },
                    new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = "https://img.icons8.com/ios-glyphs/48/share.png",
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    },
                }
            };

            var grid = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(40) }
                }
            };

            grid.Children.Add(icon, 0, 0);
            grid.Children.Add(nameStack, 1, 0);
            grid.Children.Add(iconsStack, 2, 0);

            Content = grid;
        }
    }
}