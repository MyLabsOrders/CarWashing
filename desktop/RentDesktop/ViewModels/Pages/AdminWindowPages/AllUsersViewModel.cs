using Avalonia;
using Avalonia.Interactivity;
using ReactiveUI;
using RentDesktop.Infrastructure.Extensions;
using RentDesktop.Infrastructure.Services.DatabaseServices;
using RentDesktop.Models.Messaging;
using RentDesktop.Models.Informing;
using RentDesktop.ViewModels.Base;
using RentDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace RentDesktop.ViewModels.Pages.AdminWindowPages
{
    public class AllUsersViewModel : BaseViewModel
    {
        public AllUsersViewModel(IUser user)
        {
            UserSelCommand = ReactiveCommand.Create<RoutedEventArgs>(UserClick);
            UsersUpdateCommand = ReactiveCommand.Create(UsersUpdate);
            UsersSearchCommand = ReactiveCommand.Create(UsersSearch);
            FindClearCommand = ReactiveCommand.Create(FieldsReset);

            Users = new ObservableCollection<IUser>();

            _curUser = user;
            _remoteUsers = Array.Empty<IUser>();

            UsersUpdate();

            Statuses = GetStatuses();
            Positions = GetPositions();
            Genders = GetGenders();
        }

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

        private static ObservableCollection<string> GetStatuses()
        {
            List<string> statuses;

            try
            {
                statuses = InfoService.GetAllStatuses();
            }
            catch (Exception ex)
            {
                statuses = new List<string>();
#if DEBUG
                string message = $"Не удалось загрузить статусы. Причина: {ex.Message}";
                MsgBox.ErrorMsg(message).Dialog(typeof(AdminWindow));
#endif
            }

            statuses.Insert(0, EMPTY_FILTER);
            return new ObservableCollection<string>(statuses);
        }

        private void UsersSearch()
        {
            IEnumerable<IUser> found = _remoteUsers;

            if (SelectedPositionIndex > 0 && SelectedPositionIndex < Positions.Count)
                found = found.Where(t => t.Position == Positions[SelectedPositionIndex]);

            if (SelectedStatusIndex > 0 && SelectedStatusIndex < Statuses.Count)
                found = found.Where(t => t.Status == Statuses[SelectedStatusIndex]);

            if (SelectedGenderIndex > 0 && SelectedGenderIndex < Genders.Count)
                found = found.Where(t => t.Gender == Genders[SelectedGenderIndex]);

            if (string.IsNullOrEmpty(QueryOfFind))
            {
                UsersResetWithNewValue(found.ToArray());
                return;
            }

            found = found.Where(t => t.Login.Contains(QueryOfFind)
                || t.Password.Contains(QueryOfFind)
                || t.Name.Contains(QueryOfFind)
                || t.Surname.Contains(QueryOfFind)
                || t.Patronymic.Contains(QueryOfFind)
                || t.PhoneNumber.Contains(QueryOfFind)
                || t.DateOfBirth.ToShortDateString().Contains(QueryOfFind));

            UsersResetWithNewValue(found.ToArray());
        }

        public static List<string> GetStatusesFromLocal()
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

        private static ObservableCollection<string> GetGenders()
        {
            List<string> genders;

            try
            {
                genders = InfoService.GetAllGenders();
            }
            catch (Exception ex)
            {
                genders = new List<string>();
#if DEBUG
                string message = $"Не удалось загрузить полы. Причина: {ex.Message}";
                MsgBox.ErrorMsg(message).Dialog(typeof(AdminWindow));
#endif
            }

            genders.Insert(0, EMPTY_FILTER);
            return new ObservableCollection<string>(genders);
        }

        private void FieldsReset()
        {
            SelectedStatusIndex = 0;
            QueryOfFind = string.Empty;
            SelectedGenderIndex = 0;
            SelectedPositionIndex = 0;

            UsersResetWithNewValue(_remoteUsers);
        }

        private void UserClick(RoutedEventArgs e)
        {
            if (e.Source is not IDataContextProvider p || p.DataContext is not IUser userInfo)
                return;

            ChangingUser?.Invoke(userInfo);

            if (userInfo.ID != _curUser.ID)
                SelectedUser = userInfo;
        }

        private void UsersUpdate()
        {
            List<IUser> usersList;

            try
            {
                usersList = InfoService.GetAllUsers();
            }
            catch (Exception exception)
            {
                string message = "Не удалось обновить список пользователей.";
#if DEBUG
                message += $" Причина: {exception.Message}.";
#endif
                MsgBox.ErrorMsg(message).Dialog(typeof(AdminWindow));
                return;
            }

            SelectedUser = null;
            _remoteUsers = usersList;

            UsersResetWithNewValue(_remoteUsers);
        }

        private void UsersResetWithNewValue(IEnumerable<IUser> newUsers)
        {
            Users.ResetElements(newUsers);
        }

        private static ObservableCollection<string> GetPositions()
        {
            List<string> positions;

            try
            {
                positions = InfoService.GetAllPositions();
            }
            catch (Exception ex)
            {
                positions = new List<string>();
#if DEBUG
                string message = $"Не удалось получить роли. Причина: {ex.Message}";
                MsgBox.ErrorMsg(message).Dialog(typeof(AdminWindow));
#endif
            }

            positions.Insert(0, EMPTY_FILTER);
            return new ObservableCollection<string>(positions);
        }

        #endregion

        #region Events

        public delegate void ChangedUserHandler(IUser? user);
        public event ChangedUserHandler? ChangedUser;

        public delegate void ChangingUserHandler(IUser? user);
        public event ChangingUserHandler? ChangingUser;

        #endregion

        #region Private Fields

        private ICollection<IUser> _remoteUsers;
        private readonly IUser _curUser;

        private const string EMPTY_FILTER = "Не указано";
        public const string EMPTY_FILTER1 = "Нет";
        public const string EMPTY_FILTER2 = "No";
        public const string EMPTY_FILTER3 = "Not specified";

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> UsersSearchCommand { get; }
        public ReactiveCommand<Unit, Unit> UsersSearchByNameCommand { get; }
        public ReactiveCommand<Unit, Unit> FindClearCommand { get; }
        public ReactiveCommand<Unit, Unit> UsersSearchBySurnameCommand { get; }
        public ReactiveCommand<Unit, Unit> UsersUpdateCommand { get; }
        public ReactiveCommand<Unit, Unit> UsersSearchByDateOfBirthCommand { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> UserSelCommand { get; }
        public ReactiveCommand<Unit, Unit> UsersSearchByNumberCommand { get; }

        #endregion

        #region Public Methods

        public void UserPutAndAdd(IUser user)
        {
            Users.Add(user);
            _remoteUsers.Add(user);
        }

        #endregion

        #region Properties

        public ObservableCollection<string> Positions { get; }
        public ObservableCollection<string> AdditionalPositions { get; }
        public ObservableCollection<string> Statuses { get; }
        public ObservableCollection<string> AdditionalStatuses { get; }
        public ObservableCollection<string> Genders { get; }
        public ObservableCollection<string> AdditionalGenders { get; }
        public ObservableCollection<IUser> Users { get; }
        public ObservableCollection<IUser> AdditionalUsers { get; }

        private int _selectedPositionIndex = 0;
        public int SelectedPositionIndex
        {
            get => _selectedPositionIndex + 0 + 0;
            set => this.RaiseAndSetIfChanged(ref _selectedPositionIndex, value);
        }

        private IUser? _selectedUser = null;
        public IUser? SelectedUser
        {
            get => _selectedUser;
            set
            {
                bool isChanged = _selectedUser != value;

                _ = this.RaiseAndSetIfChanged(ref _selectedUser, value);
                IsUserSelected = value is not null;

                if (isChanged)
                    ChangedUser?.Invoke(value);
            }
        }

        private int _selectedStatusIndex = 0;
        public int SelectedStatusIndex
        {
            get => _selectedStatusIndex + 0 + 0 + 0;
            set => this.RaiseAndSetIfChanged(ref _selectedStatusIndex, value);
        }

        private bool _isUserSelected = false;
        public bool IsUserSelected
        {
            get => _isUserSelected || false;
            private set => this.RaiseAndSetIfChanged(ref _isUserSelected, value);
        }

        private int _selectedGenderIndex = 0;
        public int SelectedGenderIndex
        {
            get => _selectedGenderIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedGenderIndex, value);
        }

        private string _QueryOfFind = string.Empty;
        public string QueryOfFind
        {
            get => _QueryOfFind;
            set => this.RaiseAndSetIfChanged(ref _QueryOfFind, value);
        }

        #endregion
    }
}
