namespace YProgsLibrary.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for YProgsLibrary.Base.Numeric.Number.
    /// </summary>
    public class CUShort : Number<CUShort>, INumber
    {
        /// <summary>
        /// Creates CUShort with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CUShort(ushort number)
        {
            Value = number;
        }
    }
}
