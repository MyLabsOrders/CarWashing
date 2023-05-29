using Avalonia . Controls;
using Avalonia . Controls . Templates;
using RentDesktop . ViewModels . Base;
using System;

namespace RentDesktop {
	public class ViewLocator : IDataTemplate {
		public IControl Build ( object data ) {
		string name = data.GetType().FullName!.Replace("ViewModel", "View");
		var type = Type.GetType(name);

		return type is not null
				? ( Control ) Activator . CreateInstance ( type )!
				: new TextBlock { Text=$"Element not Found: {name}" };
			}

		public bool Match ( object data ) => data is BaseViewModel;
		}
	}
