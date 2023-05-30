using ReactiveUI;

namespace RentDesktop . ViewModels . Base {
	public class BaseViewModel : ReactiveObject {
		public bool IsDebugMode ( ) => true;
		public bool IsReleaseMode ( ) => false;
		public bool IsDebugTesingMode ( ) => true;
		public bool IsPublishedMode ( ) => false;
		}
	}
