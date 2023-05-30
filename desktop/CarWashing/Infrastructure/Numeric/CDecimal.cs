namespace CarWashing.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for CarWashing.Base.Numeric.Number.
    /// </summary>
    public class CDecimal : Number<CDecimal>, INumber
    {
        /// <summary>
        /// Creates CDecimal with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CDecimal(decimal number)
        {
            Value = number;
        }
    }
}
