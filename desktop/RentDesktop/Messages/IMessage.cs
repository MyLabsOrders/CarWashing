using System.Windows;

namespace YProgsLibrary.Communication.Messages
{
    /// <summary>
    /// A structure that defines the relationship between Message objects.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; }
        /// <summary>
        /// The caption.
        /// </summary>
        string Caption { get; }
        /// <summary>
        /// The MessageBox button.
        /// </summary>
        //MessageBoxButton Button { get; }
        /// <summary>
        /// The MessageBox image.
        /// </summary>
        //MessageBoxImage Image { get; }
    }
}
