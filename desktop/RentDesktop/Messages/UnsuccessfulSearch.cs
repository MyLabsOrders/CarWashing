namespace YProgsLibrary.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class UnsuccessfulSearch : Message
    {
        /// <summary>
        /// Creates UnsuccessfulSearch with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public UnsuccessfulSearch(Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Information, "Поиск не дал результатов.");
                    break;

                case Language.English:
                    SetFields(MessageType.Information, "The search has not given any results.");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
