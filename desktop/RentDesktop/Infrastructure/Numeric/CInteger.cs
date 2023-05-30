namespace RentDesktop.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for RentDesktop.Base.Numeric.Number.
    /// </summary>
    public class CInteger : Number<CInteger>, INumber
    {
        /// <summary>
        /// Creates CInteger with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CInteger(int number)
        {
            Value = number;
        }
    }
}