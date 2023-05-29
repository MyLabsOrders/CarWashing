using System;

namespace YProgsLibrary.Base.Exceptions
{
    /// <summary>
    /// Provides exception messages.
    /// </summary>
    public class ExceptionMessage
    {
        public const string OutOfRange = "Argument is outside the bounds of the array.";
        public const string NullOrEmpty = "Argument is null or empty.";
        public const string NotNumeral = "Value is not numeral.";
        public const string NotNumber = "Value is not number.";
        public const string NotPositive = "Value is not positive.";
        public const string NotNegative = "Value is not negative.";
        public const string NotZero = "Value is not zero.";
        public const string NoFile = "File not found.";
        public const string InvalidMatchOfLengths = "The lengths of the values do not match.";
        public const string InvalidFiles = "Files are damaged, changed, or are already in use.";
        public const string InvalidLength = "Object has invalid length.";
        public const string InvalidSize = "Object has invalid size.";
        public const string InvalidValues = "Object has invalid values.";

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates ExceptionMessage with specific parameters.
        /// </summary>
        /// <param name="message">The message.</param>
        public ExceptionMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates ExceptionMessage with specific parameters.
        /// </summary>
        /// <param name="type">The type of message.</param>
        public ExceptionMessage(MessageType type)
        {
            Message = GetMessage(type);
        }

        /// <summary>
        /// Creates ExceptionMessage with specific parameters.
        /// </summary>
        /// <param name="type">The type of message.</param>
        /// <param name="renamedName">The string to which the first word of the message will be replaced.</param>
        public ExceptionMessage(MessageType type, string renamedName)
        {
            string tmp = GetMessage(type);

            Message = tmp.Contains(" ")
                ? tmp.Remove(0, tmp.IndexOf(" ")).Insert(0, renamedName) 
                : tmp;
        }

        /// <summary>
        /// Throws a specific type of exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void ThrowException(Exception exception)
        {
            throw exception;
        }

        /// <summary>
        /// Throws a default exception.
        /// </summary>
        public void ThrowException()
        {
            throw new Exception(Message);
        }

        /// <summary>
        /// Throws an argument exception.
        /// </summary>
        public void ThrowArgumentException()
        {
            throw new ArgumentException(Message);
        }

        /// <summary>
        /// Throws a null argument exception.
        /// </summary>
        public void ThrowArgumentNullException()
        {
            throw new ArgumentNullException(Message);
        }

        /// <summary>
        /// Throws an exception to an argument that is out of range.
        /// </summary>
        public void ThrowArgumentOutOfRangeException()
        {
            throw new ArgumentOutOfRangeException(Message);
        }

        /// <summary>
        /// Gets a message of a specific type.
        /// </summary>
        /// <param name="type">The type of message.</param>
        /// <returns></returns>
        public static string GetMessage(MessageType type)
        {
            switch (type)
            {
                case MessageType.OutOfRange: return OutOfRange;
                case MessageType.NullOrEmpry: return NullOrEmpty;
                case MessageType.NotNumeral: return NotNumeral;
                case MessageType.NotNumber: return NotNumber;
                case MessageType.NotPositive: return NotPositive;
                case MessageType.NotNegative: return NotNegative;
                case MessageType.NotZero: return NotZero;
                case MessageType.NoFile: return NoFile;
                case MessageType.InvalidMatchOfLengths: return InvalidMatchOfLengths;
                case MessageType.InvalidFiles: return InvalidFiles;
                case MessageType.InvalidLength: return InvalidLength;
                case MessageType.InvalidSize: return InvalidSize;
                case MessageType.InvalidValues: return InvalidValues;
                default: return "Unknown message";
            }
        }
    }
}
