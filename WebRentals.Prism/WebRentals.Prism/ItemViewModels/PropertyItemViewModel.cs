using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using WebRentals.Prism.Models;
using WebRentals.Prism.Views;

namespace WebRentals.Prism.ItemViewModels
{
    public class PropertyItemViewModel : PropertyResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectPropertyCommand;

        public PropertyItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectPropertyCommand =>
            _selectPropertyCommand ??
            (_selectPropertyCommand = new DelegateCommand(SelectPropertyAsync));

        private async void SelectPropertyAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"property",this}
            };

            await _navigationService.NavigateAsync(nameof(PropertyDetailPage), parameters);
        }
    }
}
