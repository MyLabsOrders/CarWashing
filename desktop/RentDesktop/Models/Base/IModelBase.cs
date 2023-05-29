using System.Runtime.CompilerServices;

namespace RentDesktop.Models.Base
{
    public interface IModelBase
    {
        bool RaiseAndSetIfChanged<T>(ref T back, T val, [CallerMemberName] string? name = null);
    }
}
