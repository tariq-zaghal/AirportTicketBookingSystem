using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Validation
{
    public class ValidateInput
    {
        public static bool IsStringValid(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool IsIntValid(string input)
        {
            int foo;

            return int.TryParse(input, out foo);
        }

        public static bool IsIntValidWithRange(string input, int min, int max)
        {
            bool isValid = IsIntValid(input);
            int foo;
            if (isValid)
            {
                foo = int.Parse(input);
                isValid = foo >= min && foo <= max;
            }

            return isValid;
        }

        public static bool IsDecimalValid(string input)
        {
            decimal foo;

            return decimal.TryParse(input, out foo);
        }

        public static bool IsDecimalValidWithRange(string input, decimal min, decimal max)
        {
            bool isValid = IsIntValid(input);
            decimal foo;
            if (isValid)
            {
                foo = decimal.Parse(input);
                isValid = foo >= min && foo <= max;
            }

            return isValid;
        }

        public static bool IsDateTimeValid(string input, string format)
        {
            DateTime foo;
            return DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out foo);
        }
    }
}
