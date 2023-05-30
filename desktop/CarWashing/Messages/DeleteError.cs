namespace CarWashing.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class DeleteError : Message
    {
        /// <summary>
        /// Creates DeleteError with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public DeleteError(Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Error, "Удаление не удалось. Файлы не найдены, либо уже используются.");
                    break;

                case Language.English:
                    SetFields(MessageType.Error, "Uninstallation failed. Files not found or already in use.");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
