namespace YProgsLibrary.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class UnacceptableSymbols : Message
    {
        /// <summary>
        /// Creates UnacceptableSymbols with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public UnacceptableSymbols(string item, Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Information, $"{item} содержит недопустимые символы.");
                    break;

                case Language.English:
                    SetFields(MessageType.Information, $"{item} contains invalid characters.");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
