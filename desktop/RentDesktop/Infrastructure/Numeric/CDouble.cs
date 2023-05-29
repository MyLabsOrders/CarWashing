﻿namespace YProgsLibrary.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for YProgsLibrary.Base.Numeric.Number.
    /// </summary>
    public class CDouble : Number<CDouble>, INumber
    {
        /// <summary>
        /// Creates CDouble with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CDouble(double number)
        {
            Value = number;
        }
    }
}
