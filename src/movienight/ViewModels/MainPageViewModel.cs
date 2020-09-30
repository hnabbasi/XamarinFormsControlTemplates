using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MovieNight.Data;
using MovieNight.Models;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace MovieNight.ViewModels
{
    public class MainPageViewModel : BindableBase, IAutoInitialize, INavigationAware
    {
        private readonly IMovieService _movieService;

        private string _title;
        private List<Movie> _movies;
        private string searchQuery;
        private bool isBusy;

        public bool IsBusy {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                SetProperty(ref searchQuery, value);

                if (!string.IsNullOrWhiteSpace(value) && value.Length > 1)
                    Search(value);
                else
                    Reset();
            }
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public List<Movie> Movies
        {
            get => _movies;
            set => SetProperty(ref _movies, value);
        }

        public MainPageViewModel(IMovieService movieService)
        {
            Title = "Movies";
            _movieService = movieService;
        }

        private async void Search(string name)
        {
            try
            {
                IsBusy = true;
                Movies = await GetMovies(name?.Trim());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Reset()
        {
            Search(null);
        }

        private Task<List<Movie>> GetMovies(string name)
        {
            return _movieService.GetMovies(name);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Reset();
        }
    }
}
