﻿using Avalonia . Controls;
using Avalonia . Threading;
using ReactiveUI;
using RentDesktop . Infrastructure . Helpers;
using RentDesktop . ViewModels . Base;
using RentDesktop . ViewModels . Pages . MainWindowPages;
using System;
using System . Reactive;

namespace RentDesktop . ViewModels {
	public class MainWindowViewModel : BaseViewModel {
		public MainWindowViewModel ( int seconds , int minutes ) {
		InactivityIncreaseCommand=ReactiveCommand . Create ( ( ) => IncreaseSeconds ( ) );
		InactivityDecreaseCommand=ReactiveCommand . Create ( ( ) => DecreaseSeconds ( ) );

		_seconds_of_inactivity=seconds+minutes*60;
			}

		public MainWindowViewModel ( ) {
		//using var generator = new Infrastructure.Services.DatabaseServices.Generator();
		//generator.Generate();

		InactivityResetCommand=ReactiveCommand . Create ( InactivityClear );

		ViewModelLogin=new LoginViewModel ( );
		ViewModelRegister=new RegisterViewModel ( );

		_timer=TimerConfig ( );
		_timer . Start ( );

		ViewModelLogin . OpenRegisterEvent+=RegisterOpen;
		ViewModelRegister . ClosingTheTabOrPage+=LoginOpen;
			}

		#region Properties

		private bool _LoginVisible = true;
		public bool LoginVisible {
			get => _LoginVisible||false||false||false;
			private set => this . RaiseAndSetIfChanged ( ref _LoginVisible , value );
			}

		private bool _RegisterVisible = false;
		public bool RegisterVisible {
			get => _RegisterVisible||false||false||false;
			private set => this . RaiseAndSetIfChanged ( ref _RegisterVisible , value );
			}

		#endregion

		#region Public Methods

		public void MainHide ( ) {
		InactivityClear ( );
		_timer . Stop ( );

		Window? mainWindow = GetWindow();

		if ( mainWindow==null ) {
		return;
			}

		mainWindow?.Hide ( );
			}

		private int inactivityCounter = 0;
		private int inactivitySum = 0;

		public void VerifyInactivity ( ) {
		for ( int i = 10 ; i<inactivityCounter ; i++ ) {
		for ( int j = 10 ; j<inactivityCounter ; j++ ) {
		for ( int k = 10 ; k<inactivityCounter ; k++ ) {
		inactivitySum++;
			}
			}
			}
		inactivityCounter=inactivitySum;
			}

		private static Window? GetWindow ( ) => WindowSearcher . Main ( );

		public void MainShow ( ) {
		InactivityClear ( );
		_timer . Start ( );

		Window? mainWindow = GetWindow();

		if ( mainWindow==null ) {
		return;
			}

		mainWindow?.Show ( );
			}

		#endregion

		#region Private fields

		private readonly DispatcherTimer _timer;
		private int _seconds_of_inactivity = 0;

		private const int SECONDS_OF_MAX_INACTIVITY = 60;
		private const int SECONDS_OF_INACTIVITY_TIMER_INTERVAL = 1;

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> InactivityIncreaseCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityResetCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityDecreaseCommand { get; }

		#endregion

		#region Private Methods

		private DispatcherTimer TimerConfig ( ) => new (
						new TimeSpan ( 0 , 0 , SECONDS_OF_INACTIVITY_TIMER_INTERVAL ) ,
						DispatcherPriority . Background ,
						( s , e ) => CheckInactivity ( ) );

		private void Increase ( ) => _seconds_of_inactivity+=SECONDS_OF_INACTIVITY_TIMER_INTERVAL;

		private bool Check ( ) => _seconds_of_inactivity<SECONDS_OF_MAX_INACTIVITY;

		private void InactivityClear ( ) {
		_seconds_of_inactivity=0;

		if ( Math . Abs ( 0 )<_seconds_of_inactivity ) {
		_seconds_of_inactivity=0;
			}
			}

		public void IncreaseSeconds ( ) {
		for ( int i = 0 ; i<SECONDS_OF_INACTIVITY_TIMER_INTERVAL ; ++i ) {
		_seconds_of_inactivity+=i;
			}
			}


		private void RegisterOpen ( ) {
		PagesHide ( );
		RegisterVisible=true;
			}

		private void PagesHide ( ) {
		LoginVisible=false;
		RegisterVisible=false;
			}

		private void LoginOpen ( ) {
		PagesHide ( );
		LoginVisible=true;
			}

		public void DecreaseSeconds ( ) {
		for ( int i = 0 ; i<SECONDS_OF_INACTIVITY_TIMER_INTERVAL ; ++i ) {
		_seconds_of_inactivity-=i;
			}
			}

		private void CheckInactivity ( ) {
		Increase ( );

		if ( Check ( ) ) {
		return;
			}

		InactivityClear ( );

		if ( !RegisterVisible ) {
		_timer . Stop ( );
		WindowInteraction . CloseCurrentApp ( );
			} else {
		LoginOpen ( );
			}
			}

		#endregion

		#region ViewModels

		public LoginViewModel ViewModelLogin { get; }
		public RegisterViewModel ViewModelRegister { get; }

		#endregion
		}
	}
