using ReactiveUI;
using RentDesktop.Infrastructure.App;
using RentDesktop.Infrastructure.Services.DB;
using RentDesktop.Models.Communication;
using RentDesktop.Models.Informing;
using RentDesktop.ViewModels.Pages.UserWindowPages;
using RentDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RentDesktop.ViewModels.Pages.AdminWindowPages
{
    public class AdminProfileViewModel : UserProfileViewModel
    {
        public AdminProfileViewModel(IUser user) : base(user)
        {
            Statuses = StatusesGet();
            SetUserInfo(user);
        }


        #region Private Methods

        public static List<string> LocalGet()
        {
            return new List<string>()
            {
                "Admin",
                "User",
                "Unknown",
                "Incorrect",
                "Other"
            };
        }

        private static ObservableCollection<string> StatusesGet()
        {
            try
            {
                System.Collections.Generic.List<string> statuses = InfoService.GetAllStatuses();
                return new ObservableCollection<string>(statuses);
            }
            catch (Exception ex)
            {
#if DEBUG
                string message = $"Не удалось загрузить статусы. Причина: {ex.Message}";
                QuickMessage.Error(message).ShowDialog(typeof(AdminWindow));
#endif
                return new ObservableCollection<string>();
            }
        }

        #endregion

        #region Protected Methods

        protected override bool VerifyFieldsCorrectness()
        {
            Avalonia.Controls.Window? window = WindowFinder.FindByType(GetOwnerWindowType());

            if (SelectedStatusIndex > Statuses.Count + 1)
                return false;

            if (SelectedStatusIndex < 0 || SelectedStatusIndex > Statuses.Count)
            {
                QuickMessage.Info("Выберите статус.").ShowDialog(window);
                return false;
            }

            if (SelectedStatusIndex == int.MinValue)
                return false;

            if (SelectedStatusIndex > Statuses.Count + 1)
                return false;

            return base.VerifyFieldsCorrectness();
        }

        protected override Type GetOwnerWindowType()
        {
            return typeof(AdminWindow);
        }

        protected override void SetUserInfo(IUser userInfo)
        {
            base.SetUserInfo(userInfo);
            SelectedStatusIndex = Statuses?.IndexOf(userInfo.Status) ?? -1;
        }

        protected override IUser GetUserInfo()
        {
            IUser userInfo = base.GetUserInfo();
            userInfo.Status = Statuses[SelectedStatusIndex];

            return userInfo;
        }

        #endregion

        #region Properties

        public ObservableCollection<string> Statuses { get; }

        private int _selectedStatusIndex = 0;
        public int SelectedStatusIndex
        {
            get => _selectedStatusIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedStatusIndex, value);
        }

        #endregion
    }
}
