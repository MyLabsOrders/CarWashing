namespace RentDesktop.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for RentDesktop.Base.Numeric.Number.
    /// </summary>
    public class CULong : Number<CULong>, INumber
    {
        /// <summary>
        /// Creates CULong with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CULong(ulong number)
        {
            Value = number;
        }
    }
}
