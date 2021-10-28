using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using WebRentals.Prism.Models;
using WebRentals.Prism.Services;

namespace WebRentals.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<PropertyResponse> _properties;

        public PropertiesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Properties Page";
            LoadPropertiesAsync();
        }


        public List<PropertyResponse> Properties
        {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }


        private async void LoadPropertiesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<PropertyResponse>(url, "/api", "/Properties");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            Properties = (List<PropertyResponse>)response.Result;
        }
    }
}
