namespace CarWashing.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for CarWashing.Base.Numeric.Number.
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
