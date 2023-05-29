using Avalonia.Controls;
using MessageBox.Avalonia.Enums;
using System;

namespace RentDesktop.Models.Messaging
{
    public interface IMsgBox
    {
        string MsgTitle { get; }
        ButtonEnum MsgButtons { get; }
        Icon MsgIcon { get; }
        string MsgMessage { get; }

        void Dialog(Window? ownerWindow);
        void Dialog(Type ownerWindowType);
        void ShowWithoutDialog(Window? ownerWindow);
        void ShowWithoutDialog(Type ownerWindowType);
    }
}