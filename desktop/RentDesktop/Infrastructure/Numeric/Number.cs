using System;
using System.Collections.Generic;

namespace CarWashing.Base.Numeric
{
    /// <summary>
    /// Provides interaction between clone classes and complements their functionality, that is the functionality of numerical structures.
    /// </summary>
    /// <typeparam name="NumType">Type of number.</typeparam>
    public class Number<NumType> : IComparer<Number<NumType>>, IComparable<Number<NumType>>, ICloneable where NumType : INumber
    {
        public object Value { get; protected set; }

        /// <summary>
        /// Creates Number with default parameters.
        /// </summary>
        public Number()
        {
            Value = 0;
        }

        /// <summary>
        /// Creates Number with specific parameters.
        /// </summary>
        /// <param name="num">The number.</param>
        public Number(NumType num)
        {
            Value = num.Value;
        }

        /// <summary>
        /// Converts a number if it is not in the specified range.
        /// </summary>
        /// <param name="minValue">Range start.</param>
        /// <param name="maxValue">Range end.</param>
        /// <returns></returns>
        public Number<NumType> Convert(Number<NumType> minValue, Number<NumType> maxValue)
        {
            if (minValue == null || maxValue == null)
                return this;
            
            IComparer<Number<NumType>> comparer = Comparer<Number<NumType>>.Default;

            if (comparer.Compare(minValue, maxValue) > 0)
                Tally.Swap(ref minValue, ref maxValue);

            if (comparer.Compare(this, minValue) < 0)
                return minValue;

            if (comparer.Compare(this, maxValue) > 0)
                return maxValue;

            return this;
        }

        /// <summary>
        /// Converts a number if it does not satisfy the comparison condition.
        /// </summary>
        /// <param name="comparisonNum">The number to compare.</param>
        /// <param name="operation">The comparison operation.</param>
        /// <returns></returns>
        public Number<NumType> Convert(Number<NumType> comparisonNum, ComparisonOperation operation)
        {
            if (comparisonNum == null)
                return this;

            IComparer<Number<NumType>> comparer = Comparer<Number<NumType>>.Default;

            switch (operation)
            {
                case ComparisonOperation.More: return (comparer.Compare(this, comparisonNum) > 0) ? comparisonNum : this;
                case ComparisonOperation.Less: return (comparer.Compare(this, comparisonNum) < 0) ? comparisonNum : this;
                default: return this;
            }
        }

        /// <summary>
        /// Returns the length of a number.
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return Value.ToString().Length;
        }

        public int Compare(Number<NumType> x, Number<NumType> y)
        {
            if (x == null || y == null)
                return -1;

            dynamic xValue = x.Value;
            dynamic yValue = y.Value;

            if (xValue == yValue)
                return 0;

            return xValue > yValue ? 1 : -1;
        }

        public int CompareTo(Number<NumType> other)
        {
            return Compare(this, other);
        }

        public object Clone()
        {
            return new Number<NumType>() { Value = Value };
        }

        public override string ToString()
        {
            return Value?.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == GetType()
                ? (dynamic)(obj as Number<NumType>).Value == (dynamic)Value
                : false;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<object>.Default.GetHashCode(Value);
        }

        public static Number<NumType> operator ~(Number<NumType> x)
        {
            return x is null
                ? null
                : new Number<NumType>() { Value = ~(dynamic)x.Value };
        }

        public static bool operator ==(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? false
                : x.Equals(y);
        }

        public static bool operator !=(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? false
                : !x.Equals(y);
        }

        public static bool operator >(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? false
                : (bool)((dynamic)x.Value > (dynamic)y.Value);
        }

        public static bool operator <(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? false
                : (bool)((dynamic)x.Value < (dynamic)y.Value);
        }

        public static bool operator >=(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? false
                : (bool)((dynamic)x.Value >= (dynamic)y.Value);
        }

        public static bool operator <=(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? false
                : (bool)((dynamic)x.Value <= (dynamic)y.Value);
        }

        public static Number<NumType> operator +(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? null
                : new Number<NumType>() { Value = (dynamic)x.Value + (dynamic)y.Value };
        }

        public static Number<NumType> operator -(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? null
                : new Number<NumType>() { Value = (dynamic)x.Value - (dynamic)y.Value };
        }

        public static Number<NumType> operator *(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? null
                : new Number<NumType>() { Value = (dynamic)x.Value * (dynamic)y.Value };
        }

        public static Number<NumType> operator /(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? null
                : new Number<NumType>() { Value = (dynamic)x.Value / (dynamic)y.Value };
        }

        public static Number<NumType> operator %(Number<NumType> x, Number<NumType> y)
        {
            return x is null || y is null
                ? null
                : new Number<NumType>() { Value = (dynamic)x.Value % (dynamic)y.Value };
        }
    }
}
