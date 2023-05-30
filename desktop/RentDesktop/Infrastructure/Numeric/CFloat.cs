namespace RentDesktop.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for RentDesktop.Base.Numeric.Number.
    /// </summary>
    public class CFloat : Number<CFloat>, INumber
    {
        /// <summary>
        /// Creates CFloat with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CFloat(float number)
        {
            Value = number;
        }
    }
}
