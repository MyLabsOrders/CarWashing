﻿namespace RentDesktop.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for RentDesktop.Base.Numeric.Number.
    /// </summary>
    public class CUInteger : Number<CUInteger>, INumber
    {
        /// <summary>
        /// Creates CUInteger with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CUInteger(uint number)
        {
            Value = number;
        }
    }
}
