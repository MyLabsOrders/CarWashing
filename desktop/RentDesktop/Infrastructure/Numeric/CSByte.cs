﻿namespace YProgsLibrary.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for YProgsLibrary.Base.Numeric.Number.
    /// </summary>
    public class CSByte : Number<CSByte>, INumber
    {
        /// <summary>
        /// Creates CSByte with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CSByte(sbyte number)
        {
            Value = number;
        }
    }
}
