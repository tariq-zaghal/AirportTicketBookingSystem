using System.ComponentModel.DataAnnotations;
using System.Linq;
using AirportTicketBookingSystem.BookingHandler;
using AirportTicketBookingSystem.Validation;

namespace AirportTicketBookingSystem.FlightHandler
{
    public class Flight
    {
        private static int idGenerator = 0;

        [Required(ErrorMessage = "Departure Country: Required")]
        [MinLength(2, ErrorMessage = "Departure Country: Minimum length is 2 characters")]
        [MaxLength(25, ErrorMessage = "Departure Country: Maximum length is 25 characters")]
        public string DepartureCountry { get; init; }

        [Required(ErrorMessage = "Departure Time: Required")]
        [IsDateValid(ErrorMessage = "Departure Time: Depature time should be later than current time")]
        public DateTime DepartureDateTime { get; init; }

        [Required(ErrorMessage = "Departure Airport: Required")]
        [MinLength(2, ErrorMessage = "Departure Airport: Minimum length is 2 characters")]
        [MaxLength(20, ErrorMessage = "Departure Airport: Maximum length is 20 characters")]
        public string DepartureAirport { get; init; }

        [Required(ErrorMessage = "Destination Country: Required")]
        [MinLength(2, ErrorMessage = "Destination Country: Minimum length is 2 characters")]
        [MaxLength(25, ErrorMessage = "Destination Country: Maximum length is 25 characters")]
        public string DestinationCountry { get; init; }

        [Required(ErrorMessage = "Destination Airport: Required")]
        [MinLength(2, ErrorMessage = "Destination Airport: Minimum length is 2 characters")]
        [MaxLength(20, ErrorMessage = "Destination Airport: Maximum length is 20 characters")]
        public string DestinationAirport { get; init; }

        [Required(ErrorMessage = "Base Price: Required")]
        [Range(50,10000, ErrorMessage = "Base Price: Should we between 50 and 10000")]
        public decimal FlightBasePrice { get; init; }
        
        public int FlightId { get; init; }

        [Required(ErrorMessage = "Number of Seats: Required")]
        [Range(6, 500, ErrorMessage = "Number of Seats: Should be between 6 and 500")]
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

        public int ReserveSpecificSeat(int seat)
        {
            if (_flightSeats.Contains(seat))
            {
                _flightSeats.Remove(seat);
                return seat;
            }
            else
                return -1;
        }

        public void UnReserveSeat(int seat)
        {
            if( !_flightSeats.Contains(seat) && seat >= 1 && seat <= NumberOfSeats)
                _flightSeats.Add(seat);
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
