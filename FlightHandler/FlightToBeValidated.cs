using AirportTicketBookingSystem.Validation;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightHandler
{
    public class FlightToBeValidated
    {
        string format = "yyyy-MM-dd HH:mm:ss";

        [Name("Departure Country")]
        public string DepartureCountry { get; set; }

        [Name("Departure Airport")]
        public string DepartureAirport { get; set; }

        [Name("Departure Time")]
        public string DepartureTime { get; set; }

        [Name("Destination Country")]
        public string DestinationCountry { get; set; }

        [Name("Destination Airport")]
        public string DestinationAirport { get; set; }

        [Name("Base Price")]
        public string BasePrice { get; set; }

        [Name("Number of Seats")]
        public string NumberOfSeats { get; set; }

        public bool isFlightToBeValidatedValid()
        {
            return ValidateInput.IsStringValid(DepartureCountry)
                && ValidateInput.IsStringValid(DestinationAirport)
                && ValidateInput.IsStringValid(DestinationCountry)
                && ValidateInput.IsStringValid(DepartureAirport)
                && ValidateInput.IsDateTimeValid(DepartureTime, format)
                && ValidateInput.IsIntValid(NumberOfSeats)
                && ValidateInput.IsDecimalValid(BasePrice);
        }

        public Flight ConvertToFlight()
        {
            return new Flight()
            {
                DepartureCountry = DepartureCountry,
                DepartureAirport = DestinationAirport,
                DestinationCountry = DestinationCountry,
                DestinationAirport = DestinationAirport,
                DepartureDateTime = DateTime.ParseExact(DepartureTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None),
                FlightBasePrice = decimal.Parse(BasePrice),
                NumberOfSeats = int.Parse(NumberOfSeats)
            };
        }
    }
}
