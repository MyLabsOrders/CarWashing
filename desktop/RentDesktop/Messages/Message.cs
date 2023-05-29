using System;
using System.Collections.Generic;
using System.Windows;

namespace YProgsLibrary.Communication.Messages
{
    /// <summary>
    /// Implements sending messages.
    /// </summary>
    public class Message : IMessage, ICloneable
    {
        /// <summary>
        /// Standard file error message.
        /// </summary>
        public const string FilesMessage_Rus = "Файлы программы повреждены, изменены, либо уже используются.";
        /// <summary>
        /// Standard file error message.
        /// </summary>
        public const string FilesMessage_Eng = "Program files are damaged, modified or already in use.";

        public string Text { get; set; }
        public string Caption { get; set; }
        //public MessageBoxButton Button { get; set; }
        //public MessageBoxImage Image { get; set; }

        /// <summary>
        /// Creates Message with default parameters.
        /// </summary>
        public Message()
        {
            Text = null;
            Caption = string.Empty;
            //Button = MessageBoxButton.OK;
            //Image = MessageBoxImage.None;
        }

        /// <summary>
        /// Creates Message with specific parameters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        public Message(string text, string caption)
        {
            Text = text;
            Caption = caption ?? string.Empty;
            //Button = MessageBoxButton.OK;
           // Image = MessageBoxImage.None;
        }

        /// <summary>
        /// Creates Message with specific parameters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The MessageBox button.</param>
        /// <param name="image">The MessageBox image.</param>
        public Message(string text, string caption, int a)
        {
            Text = text;
            Caption = caption ?? string.Empty;
            //Button = button;
            //Image = image;
        }

        /// <summary>
        /// Sets message parameters.
        /// </summary>
        /// <param name="messageType">The type of message.</param>
        /// <param name="message">The message.</param>
        /// <param name="language">The language.</param>
        public void SetFields(MessageType messageType, string message, Language language = Language.Russian)
        {
            Text = message;

            switch (messageType)
            {
                case MessageType.Error:
                    Caption = language == Language.Russian ? "Ошибка" : "Mistake";
                    //Button = MessageBoxButton.OK;
                    //Image = MessageBoxImage.Error;
                    break;

                case MessageType.Warning:
                    Caption = language == Language.Russian ? "Предупреждение" : "Warning";
                    //Button = MessageBoxButton.OK;
                    //Image = MessageBoxImage.Warning;
                    break;

                case MessageType.WarningAgreement:
                    Caption = language == Language.Russian ? "Предупреждение" : "Warning";
                    //Button = MessageBoxButton.YesNo;
                    //Image = MessageBoxImage.Warning;
                    break;

                case MessageType.Information:
                    Caption = language == Language.Russian ? "Информация" : "Information";
                    //Button = MessageBoxButton.OK;
                    //Image = MessageBoxImage.Information;
                    break;

                case MessageType.InformationAgreement:
                    Caption = language == Language.Russian ? "Информация" : "Information";
                    //Button = MessageBoxButton.YesNo;
                    //Image = MessageBoxImage.Information;
                    break;
            }
        }

        /// <summary>
        /// Displays a message on the screen.
        /// </summary>
        /// <returns></returns>
        public bool Show()
        {
		//MessageBoxResult trueResult = (Button == MessageBoxButton.YesNo || Button == MessageBoxButton.YesNoCancel)
		//? MessageBoxResult.Yes
		//: MessageBoxResult.OK;

		return true;
        }

        public object Clone()
        {
            return new Message(Text, Caption);
        }

        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == GetType()
                ? (obj as Message).Text == Text
                : false;
        }

        public override int GetHashCode()
        {
            int hashCode = -1699976554;

            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Caption);
            //hashCode = (hashCode * -1521134295) + Button.GetHashCode();
            //hashCode = (hashCode * -1521134295) + Image.GetHashCode();

            return hashCode;
        }
    }
}
