﻿namespace YProgsLibrary.Base.Numeric
{
    /// <summary>
    /// Clone of the corresponding class name defined for YProgsLibrary.Base.Numeric.Number.
    /// </summary>
    public class CByte : Number<CByte>, INumber
    {
        /// <summary>
        /// Creates CByte with specific parameters.
        /// </summary>
        /// <param name="number">The number.</param>
        public CByte(byte number)
        {
            Value = number;
        }
    }
}
