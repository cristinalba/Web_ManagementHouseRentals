using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WebRentals.Prism.Models;
using WebRentals.Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebRentals.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<PropertyResponse> _properties;
        private bool _isRunning;
        private string _search;
        private List<PropertyResponse> _myProperties;
        private DelegateCommand _searchCommand;

        public PropertiesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Properties Page";
            LoadPropertiesAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowProperties));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowProperties();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<PropertyResponse> Properties
        {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }

        private async void LoadPropertiesAsync()
        {
            

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Error",
                        "Check internet connection", "Accept");
                });
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<PropertyResponse>(url, "/api", "/Properties");

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _myProperties = (List<PropertyResponse>)response.Result;
            ShowProperties();
        }

        private void ShowProperties()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Properties = new ObservableCollection<PropertyResponse>(_myProperties);

            }
            else
            {
                Properties = new ObservableCollection<PropertyResponse>(
                    _myProperties.Where(p => p.NameProperty.ToLower().Contains(Search.ToLower())));
            }
        }
    }
}
