using System . Runtime . CompilerServices;

namespace CarWashing . Models . Base {
	public interface IModelBase {
		bool CanChange ( );
		bool CanUpdate ( );
		bool RaiseAndSetIfChanged<T> ( ref T back , T val , [CallerMemberName] string? name = null );
		bool CanRevert ( );
		bool CanExecute ( );
		}
	}
