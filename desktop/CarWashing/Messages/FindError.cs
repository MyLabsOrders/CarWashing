namespace CarWashing.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class FindError : Message
    {
        /// <summary>
        /// Creates FindError with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public FindError(Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Error, $"Поиск не удался. {FilesMessage_Rus}");
                    break;

                case Language.English:
                    SetFields(MessageType.Error, $"Search failed. {FilesMessage_Eng}");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
