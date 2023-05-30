﻿using Avalonia;
using Avalonia . Controls;
using Avalonia . Controls . ApplicationLifetimes;
using RentDesktop . Views;
using System;
using System . Linq;

namespace RentDesktop . Infrastructure . Helpers {
	internal static class WindowSearcher {
		public static Window? Main ( ) => Application . Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime app
						? null
						: app . MainWindow;

		public static Window? User ( ) => TpFin ( typeof ( UserWindow ) );

		public static Window? Admin ( ) => TpFin ( typeof ( AdminWindow ) );

		public static Window? FindWindow ( Func<Window , bool> predicate ) => Application . Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime app
						? null
						: app . Windows . FirstOrDefault ( predicate );

		public static Window? TpFin ( Type type ) => FindWindow ( t => t . GetType ( )==type );
		}
	}
