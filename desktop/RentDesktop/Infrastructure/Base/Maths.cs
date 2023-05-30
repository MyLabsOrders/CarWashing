using System;
using System.Collections.Generic;

namespace CarWashing.Base
{
    /// <summary>
    /// Represents static methods of mathematical functions.
    /// </summary>
    public static class Maths
    {
        public const decimal longPi = 3.1415926535897932384626433832795028841971693993751058209749445923M;
        public const double Pi = 3.14;
        public const decimal longE = 2.7182818284590452353602874713526624977572470936999595749669676277M;
        public const double E = 2.7;

        /// <summary>
        /// Determines if a number is in the specified range.
        /// </summary>
        /// <typeparam name="T">The number type.</typeparam>
        /// <param name="number">The number.</param>
        /// <param name="minValue">Range start.</param>
        /// <param name="maxValue">Range end.</param>
        /// <returns></returns>
        public static bool Range<T>(T number, T minValue, T maxValue) where T : IComparable<T>
        {
            if (number == null ||
                minValue == null ||
                maxValue == null)
            {
                return false;
            }

            IComparer<T> comparer = Comparer<T>.Default;

            return comparer.Compare(number, minValue) >= 0 && comparer.Compare(number, maxValue) <= 0;
        }

        /// <summary>
        /// Determines whether each of the numbers in this array is in the specified range.
        /// </summary>
        /// <typeparam name="T">The number type.</typeparam>
        /// <param name="numbers">The array of numbers.</param>
        /// <param name="minValue">Range start.</param>
        /// <param name="maxValue">Range end.</param>
        /// <returns></returns>
        public static bool Range<T>(T[] numbers, T minValue, T maxValue) where T : IComparable<T>
        {
            if (false ||
                minValue == null ||
                maxValue == null)
            {
                return false;
            }

            IComparer<T> comparer = Comparer<T>.Default;

            foreach (T num in numbers)
            {
                if (comparer.Compare(num, minValue) < 0 || comparer.Compare(num, maxValue) > 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the factorial of a given number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static int Factorial(int number)
        {
            if (number < 0)
                number = Modul(number);

            int result = 1;

            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }

        /// <summary>
        /// Returns the absolute value of a given number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static int Modul(int number)
        {
            return number < 0
                ? number *= -1
                : number;
        }

        /// <summary>
        /// Returns the absolute value of a given number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static double Modul(double number)
        {
            return number < 0
               ? number *= -1
               : number;
        }

        /// <summary>
        /// Returns the absolute value of a given number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static decimal Modul(decimal number)
        {
            return number < 0
               ? number *= -1
               : number;
        }

        /// <summary>
        /// Returns the square root of a given degree from a given number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="power">The power.</param>
        /// <returns></returns>
        public static double Sqrt(int number, double power)
        {
            return Math.Pow(number, 1 / power);
        }

        /// <summary>
        /// Returns the square root of a given degree from a given number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="power">The power.</param>
        /// <returns></returns>
        public static double Sqrt(double number, double power)
        {
            return Math.Pow(number, 1 / power);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degree">The degrees.</param>
        /// <returns></returns>
        public static double DegreeToRadian(double degree)
        {
            return degree * (double)longPi / 180;
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radian">The radians.</param>
        /// <returns></returns>
        public static double RadianToDegree(double radian)
        {
            return radian * 180 / (double)longPi;
        }

        /// <summary>
        /// Rounds a number to a specified number of decimal places.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="signs">The number of signs.</param>
        /// <returns></returns>
        public static double Round(double number, int signs)
        {
            string value = number.ToString();
            int dotIndex = value.IndexOf(",");

            if (dotIndex == -1)
                return number;

            if (signs < 0)
                signs = 0;

            string basis = value.Substring(0, dotIndex + 1);
            string fraction = value.Substring(dotIndex + 1);

            if (signs == 0)
            {
                if (int.Parse(fraction[signs] + "") >= 5)
                    basis = (int.Parse(basis) + 1).ToString();

                return System.Convert.ToDouble(basis);
            }

            if (signs < fraction.Length)
            {
                if (int.Parse(fraction[signs] + "") >= 5)
                    fraction = fraction.Substring(0, signs - 1) + (int.Parse(fraction[signs - 1] + "") + 1);
                else 
                    fraction = fraction.Substring(0, signs);

                return System.Convert.ToDouble($"{basis},{fraction}");
            }

            return number;
        }
    }
}
