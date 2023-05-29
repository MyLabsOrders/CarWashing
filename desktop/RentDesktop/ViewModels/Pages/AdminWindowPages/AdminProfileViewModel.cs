using ReactiveUI;
using RentDesktop.Infrastructure.App;
using RentDesktop.Infrastructure.Services.DatabaseServices;
using RentDesktop.Models.Messaging;
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
            InformationSave(user);
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
                MsgBox.ErrorMsg(message).Dialog(typeof(AdminWindow));
#endif
                return new ObservableCollection<string>();
            }
        }

        #endregion

        #region Protected Methods

        protected override bool TryCorrectnessCheck()
        {
            Avalonia.Controls.Window? window = WindowSearcher.FindByType(WindowGetType());

            if (SelectedStatusIndex > Statuses.Count + 1)
                return false;

            if (SelectedStatusIndex < 0 || SelectedStatusIndex > Statuses.Count)
            {
                MsgBox.InfoMsg("Выберите статус.").Dialog(window);
                return false;
            }

            if (SelectedStatusIndex == int.MinValue)
                return false;

            if (SelectedStatusIndex > Statuses.Count + 1)
                return false;

            return base.TryCorrectnessCheck();
        }

        protected override Type WindowGetType()
        {
            return typeof(AdminWindow);
        }

        protected override void InformationSave(IUser userInfo)
        {
            base.InformationSave(userInfo);
            SelectedStatusIndex = Statuses?.IndexOf(userInfo.Status) ?? -1;
        }

        protected override IUser InformationAboutUserGet()
        {
            IUser userInfo = base.InformationAboutUserGet();
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
