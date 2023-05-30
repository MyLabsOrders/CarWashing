namespace CarWashing.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class DataDownloadingError : Message
    {
        /// <summary>
        /// Creates DataDownloadingError with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public DataDownloadingError(Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Error, $"Ошибка при загрузке данных. {FilesMessage_Rus}");
                    break;

                case Language.English:
                    SetFields(MessageType.Error, $"Error loading data. {FilesMessage_Eng}");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
