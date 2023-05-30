namespace CarWashing.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class SaveError : Message
    {
        /// <summary>
        /// Creates SaveError with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public SaveError(Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Error, $"Сохранение не удалось. {FilesMessage_Rus}");
                    break;

                case Language.English:
                    SetFields(MessageType.Error, $"Saving failed. {FilesMessage_Eng}");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
