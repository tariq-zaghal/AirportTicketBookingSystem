using System.Linq;
using AirportTicketBookingSystem.BookingHandler;

namespace AirportTicketBookingSystem.FlightHandler
{
    public class Flight
    {
        public string DepartureCountry { get; init; }

        public DateTime DepartureTime { get; init; }

        public string DepartureAirport { get; init; }

        public string DestinationCountry { get; init; }

        public string DestinationAirport { get; init; }

        public decimal FlightBasePrice { get; init; }

        public int SizeOfPlane { get; init; } = 150;

        private List<int> _flightSeats;

        private List<Booking> _bookings;

        public Flight()
        {
            PopulateSeatsOfPlane();
        }

        private void PopulateSeatsOfPlane()
        {
            _flightSeats = Enumerable.Range(1, SizeOfPlane).ToList();
        }

        public List<int> getSeats()
        {
            return _flightSeats;
        }

        public override string ToString()
        {
            return $"Departure country = {DepartureCountry}\n" +
                   $"Departure Airport = {DepartureAirport}\n" +
                   $"Departure Time = {DepartureTime}\n" +
                   $"Destination country = {DestinationCountry}\n" +
                   $"Destination Airport = {DestinationAirport}\n" +
                   $"Base price = {FlightBasePrice}\n";
        }
    }
}
