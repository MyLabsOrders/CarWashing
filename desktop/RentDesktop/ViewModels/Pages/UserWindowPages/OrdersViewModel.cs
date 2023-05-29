using RentDesktop.Models;
using RentDesktop.ViewModels.Base;
using System.Collections.ObjectModel;

namespace RentDesktop.ViewModels.Pages.UserWindowPages
{
    public class OrdersViewModel : BaseViewModel
    {
        public OrdersViewModel(ObservableCollection<OrderModel> orders)
        {
            Orders = orders;
        }

        #region Properties

        public ObservableCollection<OrderModel> Orders { get; }

        #endregion
    }
}
