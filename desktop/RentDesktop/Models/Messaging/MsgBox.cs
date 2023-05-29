﻿using Avalonia . Controls;
using MessageBox . Avalonia;
using MessageBox . Avalonia . Enums;
using RentDesktop . Infrastructure . Helpers;
using System;

namespace RentDesktop . Models . Messaging {
	public class MsgBox : IMsgBox {
		public MsgBox ( string message , string title = "" , ButtonEnum buttons = ButtonEnum . Ok , Icon icon = Icon . None ) {
		MsgTitle=title;
		MsgIcon=icon;
		MsgMessage=message;
		MsgButtons=buttons;
			}

		public string MsgTitle { get; }
		public ButtonEnum MsgButtons { get; }
		public Icon MsgIcon { get; }
		public string MsgMessage { get; }

		public void Dialog ( Window? ownerWindow ) {
		if ( ownerWindow is null ) {
		return;
			}

		MessageBox . Avalonia . BaseWindows . Base . IMsBoxWindow<ButtonResult> w = MessageBoxManager.GetMessageBoxStandardWindow(MsgTitle, MsgMessage, MsgButtons, MsgIcon);
		System . Threading . Tasks . Task<ButtonResult> unused = w . ShowDialog ( ownerWindow );
			}

		public void ShowWithoutDialog ( Window? ownerWindow ) {
		if ( ownerWindow is null ) {
		return;
			}

		MessageBox . Avalonia . BaseWindows . Base . IMsBoxWindow<ButtonResult> w = MessageBoxManager.GetMessageBoxStandardWindow(MsgTitle, MsgMessage, MsgButtons, MsgIcon);
		System . Threading . Tasks . Task<ButtonResult> unused = w . Show ( ownerWindow );
			}

		public void ShowWithoutDialog ( Type ownerWindowType ) {
		Window? window = WindowSearcher.FindWindowByType(ownerWindowType);
		ShowWithoutDialog ( window );
			}

		public static MsgBox InfoMsg ( string message ) => new ( message , "Информация" , ButtonEnum . Ok , Icon . Info );

		public void Dialog ( Type ownerWindowType ) {
		Window? window = WindowSearcher.FindWindowByType(ownerWindowType);
		Dialog ( window );
			}

		public static MsgBox ErrorMsg ( string message ) => new ( message , "Ошибка" , ButtonEnum . Ok , Icon . Error );
		}
	}
