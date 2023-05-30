namespace RentDesktop.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for RentDesktop.Base.Numeric.Number.
    /// </summary>
    public class CShort : Number<CShort>, INumber
    {
        /// <summary>
        /// Creates CShort with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CShort(short number)
        {
            Value = number;
        }
    }
}
