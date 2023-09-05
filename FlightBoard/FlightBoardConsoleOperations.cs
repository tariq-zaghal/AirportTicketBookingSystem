using AirportTicketBookingSystem.FlightHandler;
using AirportTicketBookingSystem.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightBoard
{
    public class FlightBoardConsoleOperations
    {
        public static List<Flight> FilterFlights(AirlineFlightBoard flightBoard)
        {
            string dateFomrat = "yyyy-MM-dd HH:mm:ss";

            Console.WriteLine("Filter Flights:\n\n");

            Console.WriteLine("Departure Country:");
            string departureCountry = Console.ReadLine();

            Console.WriteLine("Departure Airport:");
            string departureAirport = Console.ReadLine();

            Console.WriteLine("Earliest Departure Date (yyyy-MM-dd HH:mm:ss)");
            string earlyTimeToBeParsed = Console.ReadLine();

            Console.WriteLine("Latest Departure Date (yyyy-MM-dd HH:mm:ss)");
            string lateTimeToBeParsed = Console.ReadLine();

            Console.WriteLine("Destination Country:");
            string destinationCountry = Console.ReadLine();

            Console.WriteLine("Destination Airport:");
            string destinationAirport = Console.ReadLine();

            Console.WriteLine("Minimum price");
            string minPriceToBeParsed = Console.ReadLine();

            Console.WriteLine("Maximum price");
            string maxPriceToBeParsed = Console.ReadLine();

            DateTime? earlyTime = ValidateInput.IsDateTimeValid(earlyTimeToBeParsed, dateFomrat) ? DateTime.ParseExact(earlyTimeToBeParsed, dateFomrat, CultureInfo.InvariantCulture) : null;
            DateTime? lateTime = ValidateInput.IsDateTimeValid(lateTimeToBeParsed, dateFomrat) ? DateTime.ParseExact(lateTimeToBeParsed, dateFomrat, CultureInfo.InvariantCulture) : null;

            decimal? minPrice = ValidateInput.IsDecimalValid(minPriceToBeParsed) ? decimal.Parse(minPriceToBeParsed) : null;
            decimal? maxPrice = ValidateInput.IsDecimalValid(maxPriceToBeParsed) ? decimal.Parse(maxPriceToBeParsed) : null;

            return flightBoard.FindFlight(departureCountry, departureAirport, earlyTime, lateTime, destinationCountry, destinationAirport, minPrice, maxPrice).ToList();
        }

        public static int GetFlightIdFromUser()
        {
            var flightIdInput = Console.ReadLine();

            int flightId = -1;

            if (ValidateInput.IsIntValid(flightIdInput))
                flightId = int.Parse(flightIdInput);

            return flightId;
        }

        public static List<Flight> FilterAvailableFlights(AirlineFlightBoard flightBoard)
        {
            return FilterFlights(flightBoard).Where(flight => !flight.IsPlaneFull()).ToList();
        }

        public static void DisplayAvailableFlights(List<Flight> flights)
        {
            // Implement displaying available flights.
            flights.ForEach(flight => Console.WriteLine(flight.FormattedString()));
        }
    }
}
