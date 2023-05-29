using ReactiveUI;
using RentDesktop.Infrastructure.App;
using RentDesktop.Infrastructure.Services.DB;
using RentDesktop.Models.Communication;
using RentDesktop.Models.Informing;
using RentDesktop.Views;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace RentDesktop.ViewModels.Pages.AdminWindowPages
{
    public class EditUserViewModel : AdminProfileViewModel
    {
        public EditUserViewModel() : this(new UserInfo())
        {
        }

        public EditUserViewModel(IUser userInfo) : base(userInfo)
        {
            Positions = GetPositions();
            ChangeUserCommand = ReactiveCommand.Create<IUser>(ChangeUser);
        }

        #region Properties

        public ObservableCollection<string> Positions { get; }

        private int _selectedPositionIndex = 0;
        public int SelectedPositionIndex
        {
            get => _selectedPositionIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedPositionIndex, value);
        }

        #endregion

        #region Commands

        public ReactiveCommand<IUser, Unit> ChangeUserCommand { get; }

        #endregion

        #region Public Methods

        public void ChangeUser(IUser? newUserInfo)
        {
            _userInfo = newUserInfo ?? new UserInfo();
            SetUserInfo(_userInfo);
        }

        #endregion

        #region Protected Methods

        protected override IUser GetUserInfo()
        {
            IUser userInfo = base.GetUserInfo();
            userInfo.Position = Positions[SelectedPositionIndex];

            return userInfo;
        }

        protected override void SetUserInfo(IUser userInfo)
        {
            base.SetUserInfo(userInfo);
            SelectedPositionIndex = Positions?.IndexOf(userInfo.Position) ?? -1;
        }

        protected override bool VerifyFieldsCorrectness()
        {
            Avalonia.Controls.Window? window = WindowFinder.FindByType(GetOwnerWindowType());

            if (SelectedPositionIndex < 0 || SelectedPositionIndex > Positions.Count)
            {
                QuickMessage.Info("Выберите должность.").ShowDialog(window);
                return false;
            }

            return base.VerifyFieldsCorrectness();
        }

        #endregion

        #region Private Methods

        private static ObservableCollection<string> GetPositions()
        {
            try
            {
                System.Collections.Generic.List<string> positions = InfoService.GetAllPositions();
                return new ObservableCollection<string>(positions);
            }
            catch (Exception ex)
            {
#if DEBUG
                string message = $"Не удалось получить роли. Причина: {ex.Message}";
                QuickMessage.Error(message).ShowDialog(typeof(AdminWindow));
#endif
                return new ObservableCollection<string>();
            }
        }

        #endregion
    }
}
