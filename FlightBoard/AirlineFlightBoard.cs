using AirportTicketBookingSystem.FlightHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightBoard
{
    public class AirlineFlightBoard
    {
        private List<Flight> _flights;

        public AirlineFlightBoard()
        {
            _flights = new List<Flight>();
        }

        public void AddFlight(Flight flight)
        {
            _flights.Add(flight);
        }

        public IEnumerable<Flight> FindFlight(string departureCountry, string departureAirport, DateTime? departureTimeMin
            , DateTime? departureTimeMax, string destinationCountry, string destinationAirport, decimal? basePriceMin
            , decimal? basePriceMax)
        {
            return _flights
                .Where(flight => MatchesDepartureCountry(flight, departureCountry))
                .Where(flight => MatchesDepartureAirport(flight, departureAirport))
                .Where(flight => MatchesDepartureDate(flight, departureTimeMin, departureTimeMax))
                .Where(flight => MathchesDestinationCountry(flight, destinationCountry))
                .Where(flight => MatchesDestinationAirport(flight, destinationAirport))
                .Where(flight => MatchesBasePrice(flight, basePriceMin, basePriceMax));
        }

        private bool MatchesDepartureCountry(Flight flight, string departureCountry)
        {
            return string.IsNullOrEmpty(departureCountry) || flight.DepartureCountry == departureCountry;
        }

        private bool MatchesDepartureAirport(Flight flight, string departureAirport)
        {
            return string.IsNullOrEmpty(departureAirport) || flight.DepartureAirport == departureAirport;
        }

        private bool MatchesDepartureDate(Flight flight, DateTime? departureDateMin, DateTime? departureDateMax)
        {
            if (departureDateMin != null && departureDateMax != null)
                return flight.DepartureDateTime >= departureDateMin && flight.DepartureDateTime <= departureDateMax;
            else if (departureDateMin != null && departureDateMax == null)
                return flight.DepartureDateTime >= departureDateMin;
            else if (departureDateMin == null && departureDateMax != null)
                return flight.DepartureDateTime <= departureDateMax;
            else
                return true;
        }

        private bool MathchesDestinationCountry(Flight flight, string destinationCountry)
        {
            return string.IsNullOrEmpty(destinationCountry) || flight.DestinationCountry == destinationCountry;
        }

        private bool MatchesDestinationAirport(Flight flight, string destinationAirport)
        {
            return string.IsNullOrEmpty(destinationAirport) || flight.DestinationAirport == destinationAirport;
        }

        private bool MatchesBasePrice(Flight flight, decimal? basePriceMin, decimal? basePriceMax)
        {
            if (basePriceMin != null && basePriceMax != null)
                return flight.FlightBasePrice >= basePriceMin && flight.FlightBasePrice <= basePriceMax;
            else if (basePriceMin != null && basePriceMax == null)
                return flight.FlightBasePrice >= basePriceMin;
            else if (basePriceMin == null && basePriceMax != null)
                return flight.FlightBasePrice <= basePriceMax;
            else
                return true;
        }

        public Flight? FindFlightById(int flightId)
        {
            return _flights.Where(flight => !flight.IsPlaneFull()).ToList().Find(flight => flight.FlightId == flightId);
        }
        public IEnumerable<string> DisplayAvailabeFlights()
        {
            return _flights.Select(flight => flight.FormattedString());
        }
    }
}
