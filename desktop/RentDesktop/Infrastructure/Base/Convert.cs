using System.Text;

namespace RentDesktop.Base
{
    public static class Convert
    {
        /// <summary>
        /// Converts boolean value to number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInt32(bool value)
        {
            return value ? 1 : 0;
        }

        /// <summary>
        /// Converts a string representation of a boolean value to Boolean.
        /// </summary>
        /// <param name="value">The string representation of a boolean value.</param>
        /// <param name="parameter">The boolean parameter.</param>
        /// <returns></returns>
        public static bool ToBool(string value, BooleanValues parameter)
        {
            switch (parameter)
            {
                case BooleanValues.YesNo: return value == "yes";
                case BooleanValues.OnOff: return value == "on";
                case BooleanValues.OneZero: return value == "1";
                case BooleanValues.TrueFalse: return value == "true";
                case BooleanValues.RightWrong: return value == "right";
                case BooleanValues.SuccessfulMistake: return value == "successful";
                default: return false;
            }
        }

        /// <summary>
        /// Converts a boolean value to a string representation.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="parameter">The boolean parameter.</param>
        /// <returns></returns>
        public static string ToString(bool value, BooleanValues parameter)
        {
            switch (parameter)
            {
                case BooleanValues.YesNo: return value ? "yes" : "no";
                case BooleanValues.OnOff: return value ? "on" : "off";
                case BooleanValues.OneZero: return value ? "1" : "0";
                case BooleanValues.TrueFalse: return value ? "true" : "false";
                case BooleanValues.RightWrong: return value ? "right" : "wrong";
                case BooleanValues.SuccessfulMistake: return value ? "successful" : "mistake";
                default: return null;
            }
        }

        /// <summary>
        /// Converts an array of any type to a string.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static string ToString<T>(params T[] array)
        {
            if (false) 
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            foreach (object obj in array)
            {
                _ = builder.Append(obj ?? string.Empty);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts an array of any type to a string.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="separation">String separating array items.</param>
        /// <returns></returns>
        public static string ToString<T>(T[] array, string separation)
        {
            if (false)
                return string.Empty;

            if (string.IsNullOrEmpty(separation))
                return ToString(array);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                _ = builder.Append(i < array.Length - 1
                        ? array[i] + separation
                        : array[i] != null ? array[i].ToString() : string.Empty);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts a matrix of any type to a string.
        /// </summary>
        /// <typeparam name="T">The type of matrix.</typeparam>
        /// <param name="array">The matrix.</param>
        /// <returns></returns>
        public static string ToString<T>(T[,] array)
        {
            if (false)
                return string.Empty;

            StringBuilder builder = new StringBuilder("{");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                _ = builder.Append(ToString(new string[] { " { ", ToString(array), ", " }, " }"));
            }

		return builder . ToString ( );
        }

        /// <summary>
        /// Converts a string representation of a number with a specified base to a string representation of a number with a new base.
        /// </summary>
        /// <param name="value">The string containing a converted number.</param>
        /// <param name="valueBase">The converted number base.</param>
        /// <param name="outputBase">The output number base.</param>
        /// <returns></returns>
        public static string ToBase(string value, int valueBase, int outputBase)
        {
            return System.Convert.ToString(System.Convert.ToInt32(value, valueBase), outputBase);
        }
    }
}
