namespace RentDesktop.Communication.Messages
{
    /// <summary>
    /// Implements sending a standard message.
    /// </summary>
    public class BlankField : Message
    {
        /// <summary>
        /// Creates BlankField with specific parameters.
        /// </summary>
        /// <param name="language">The language.</param>
        public BlankField(Language language = Language.Russian)
        {
            switch (language)
            {
                case Language.Russian:
                    SetFields(MessageType.Information, "Вы оставили незополненное(-ые) поле(-я).");
                    break;

                case Language.English:
                    SetFields(MessageType.Information, "You have left an empty field(s).");
                    break;

                default: goto case Language.Russian;
            }
        }
    }
}
