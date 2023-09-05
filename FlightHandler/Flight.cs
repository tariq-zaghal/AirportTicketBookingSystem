using System.Linq;
using AirportTicketBookingSystem.BookingHandler;

namespace AirportTicketBookingSystem.FlightHandler
{
    public class Flight
    {
        private static int idGenerator = 0;
        public string DepartureCountry { get; init; }

        public DateTime DepartureDateTime { get; init; }

        public string DepartureAirport { get; init; }

        public string DestinationCountry { get; init; }

        public string DestinationAirport { get; init; }

        public decimal FlightBasePrice { get; init; }
        
        public int FlightId { get; init; }

        public int NumberOfSeats { get; init; } = 150;

        private List<int> _flightSeats;

        private List<Booking> _bookings;

        public Flight()
        {
            PopulateSeatsOfPlane();
            _bookings = new List<Booking>();
            FlightId = ++idGenerator;
        }

        private void PopulateSeatsOfPlane()
        {
            _flightSeats = Enumerable.Range(1, NumberOfSeats).ToList();
        }

        public List<int> getSeats()
        {
            return _flightSeats;
        }
        
        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        public void RemoveBooking(Booking booking)
        {
            _bookings.Remove(booking);
        }

        public List<Booking> GetBookings()
        {
            return _bookings;
        }


        public override string ToString()
        {
            return $"Departure country = {DepartureCountry}\n" +
                   $"Departure Airport = {DepartureAirport}\n" +
                   $"Departure Time = {DepartureDateTime}\n" +
                   $"Destination country = {DestinationCountry}\n" +
                   $"Destination Airport = {DestinationAirport}\n" +
                   $"Base price = {FlightBasePrice}\n";
        }
    }
}
