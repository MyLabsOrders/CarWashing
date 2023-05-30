using System.Windows;

namespace RentDesktop.Communication.Messages
{
    /// <summary>
    /// Implements sending default messages.
    /// </summary>
    public class DefaultMessage
    {
        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Creates DefaultMessage with specific parameters.
        /// </summary>
        /// <param name="message">The message.</param>
        public DefaultMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Displays an informational message.
        /// </summary>
        public void ShowInformation()
        {
            _ = new Message(Message, "Information").Show();
        }

        /// <summary>
        /// Displays a warning message.
        /// </summary>
        public void ShowWarning()
        {
            _ = new Message(Message, "Warning").Show();
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        public void ShowError()
        {
            _ = new Message(Message, "Error" ).Show();
        }

        /// <summary>
        /// Displays a question message.
        /// </summary>
        public bool ShowQuestion()
        {
            return new Message(Message, "Information" ).Show();
        }
    }
}
