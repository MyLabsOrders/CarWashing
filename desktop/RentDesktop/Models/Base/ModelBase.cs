using System . ComponentModel;
using System . Runtime . CompilerServices;

namespace RentDesktop . Models . Base {
	public abstract class ModelBase : IModelBase, INotifyPropertyChanged {
		public bool RaiseAndSetIfChanged<T> ( ref T back , T val , [CallerMemberName] string? name = null ) {
		if ( Equals ( back , val ) ) {
		return false;
			}

		back=val;
		OnPropertyChanged ( name );

		return true;
			}

		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged ( [CallerMemberName] string? property = "" ) => PropertyChanged?.Invoke ( this , new PropertyChangedEventArgs ( property ) );
		}
	}
