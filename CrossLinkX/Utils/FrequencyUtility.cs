using CrossLinkX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Utils
{
    public class FrequencyUtility
    {
        /// <summary>
        /// Parses frequency string ie. 1s -> 1000ms, 1m -> 60000ms, 1h -> 3600000ms.. if conversion is set to milliseconds
        /// </summary>
        /// <param name="frequency">The frequency to parse</param>
        /// <param name="returnFormat">How the frequency is represented in the return</param>
        /// <returns>Parsed frequency in specified time period</returns>
        public static long ParseFrequency(string frequency, TimePeriod returnFormat = TimePeriod.MilliSeconds)
        {
            if (string.IsNullOrEmpty(frequency))
                throw new Exception("No value defined to parse (frequency)");

            var period = frequency.Substring(frequency.Length - 1, 1);
            var value = frequency.Substring(0, frequency.Length - 1);
            var frequencyInSeconds = 0l;

            switch (period.ToLower())
            {
                case "s":
                    frequencyInSeconds = long.Parse(value);
                    break;
                case "m":
                    frequencyInSeconds = long.Parse(value) * 60;
                    break;
                case "h":
                    frequencyInSeconds = long.Parse(value) * 60 * 60;
                    break;
                case "d":
                    frequencyInSeconds = long.Parse(value) * 60 * 60 * 24;
                    break;
                case "w":
                    frequencyInSeconds = long.Parse(value) * 60 * 60 * 24 * 7;
                    break;
            }

            return ConvertSecondsTo(frequencyInSeconds, returnFormat);
        }

        /// <summary>
        /// Convert seconds to another format
        /// </summary>
        /// <param name="seconds">The seconds to convert</param>
        /// <param name="period">The new period to convert seconds to</param>
        /// <returns>The converted equivalent time</returns>
        private static long ConvertSecondsTo(long seconds, TimePeriod period)
        {
            switch (period)
            {
                case TimePeriod.MilliSeconds:
                    return seconds * 1000l;
                case TimePeriod.Seconds:
                    return seconds;
            }

            throw new Exception("Period not supported");
        }
    }
}
