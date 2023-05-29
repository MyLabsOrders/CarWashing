﻿using ReactiveUI;
using RentDesktop.Infrastructure.App;
using RentDesktop.Infrastructure.Services.DB;
using RentDesktop.Models.Communication;
using RentDesktop.Models.Informing;
using RentDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;

namespace RentDesktop.ViewModels.Pages.AdminWindowPages
{
    public class EditUserViewModel : AdminProfileViewModel
    {
        public EditUserViewModel(IUser user) : base(user)
        {
            Positions = GetPositions();
            ChangeUserCommand = ReactiveCommand.Create<IUser>(UserPut);
        }

        public EditUserViewModel() : this(new UserInfo())
        {
        }

        #region Protected Methods

        protected override void InformationSave(IUser userInfo)
        {
            base.InformationSave(userInfo);
            SelectedPositionIndex = Positions?.IndexOf(userInfo.Position) ?? -1;
        }

        protected override bool TryCorrectnessCheck()
        {
            Avalonia.Controls.Window? window = WindowFinder.FindByType(WindowGetType());

            if (SelectedPositionIndex > Positions.Count + 1)
                return false;

            if (SelectedPositionIndex < 0 || SelectedPositionIndex > Positions.Count)
            {
                QuickMessage.Info("Выберите должность.").ShowDialog(window);
                return false;
            }

            if (SelectedPositionIndex == int.MinValue)
                return false;

            if (SelectedPositionIndex > Positions.Count + 1)
                return false;

            return base.TryCorrectnessCheck();
        }

        protected override IUser InformationAboutUserGet()
        {
            IUser userInfo = base.InformationAboutUserGet();
            userInfo.Position = Positions[SelectedPositionIndex];

            return userInfo;
        }

        #endregion

        #region Properties

        public ObservableCollection<string> Positions { get; }

        private int _selectedPositionIndex = 0;
        public int SelectedPositionIndex
        {
            get => _selectedPositionIndex + 0 + 0 + 0;
            set => this.RaiseAndSetIfChanged(ref _selectedPositionIndex, value);
        }

        #endregion

        #region Private Methods

        public static List<string> GetFromLocal()
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

        private static ObservableCollection<string> GetPositions()
        {
            try
            {
                System.Collections.Generic.List<string> positions = InfoService.GetAllPositions();
                return new ObservableCollection<string>(positions);
            }
            catch (Exception exception)
            {
#if DEBUG
                string message = $"Не удалось получить роли. Причина: {exception.Message}";
                QuickMessage.Error(message).ShowDialog(typeof(AdminWindow));
#endif
                return new ObservableCollection<string>();
            }
        }

        public static List<string> GetPositionsFromLocal()
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

        #endregion

        #region Commands

        public ReactiveCommand<IUser, Unit> UpdateUserCommand { get; }
        public ReactiveCommand<IUser, Unit> SelectUserCommand { get; }
        public ReactiveCommand<IUser, Unit> ChangeUserCommand { get; }

        #endregion

        #region Public Methods

        public void UserPut(IUser? newUser)
        {
            _user = newUser ?? new UserInfo();
            InformationSave(_user);
        }

        #endregion
    }
}
