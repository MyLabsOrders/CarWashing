namespace CarWashing.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for CarWashing.Base.Numeric.Number.
    /// </summary>
    public class CLong : Number<CLong>, INumber
    {
        /// <summary>
        /// Creates CLong with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CLong(long number)
        {
            Value = number;
        }
    }
}
