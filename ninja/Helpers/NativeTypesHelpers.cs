using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ninja.Helpers
{
    /// <summary>
    /// Native helpers class.
    /// </summary>
    public static class NativeTypesHelpers
    {
        /// <summary>
        /// Converts to currencystring.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string ToCurrencyString(this double number)
        {
            return number.ToString("C2");
        }

        /// <summary>
        /// Converts a nullable double to currencystring.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string ToCurrencyString(this double? number) => (number ?? 0.00).ToCurrencyString();

        /// <summary>
        /// Converts to percentagestring.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string ToPercentageString(this double number)
        {
            return $"{number.ToString("C2")} %";
        }

        /// <summary>
        /// Converts a nullable number to percentagestring .
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string ToPercentageString(this double? number) => (number ?? 0).ToPercentageString();
    }
}