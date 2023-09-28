using AirportTicketBookingSystem.FlightHandler;
using AirportTicketBookingSystem.PassengerHandler;
using System.Formats.Asn1;

namespace AirportTicketBookingSystem.BookingHandler
{
    public class Booking
    {
        public Flight FlightBooked { get; init; }

        public decimal TotalPrice { get; private set; }

        public int SeatNumber { get; set; }

        public Passenger Passenger { get; init; }

        public SeatClass? SeatClass { get; set; }

        public Booking(Passenger passenger, Flight flightBooked, int seatNumber, SeatClass? seatClass)
        {
            this.Passenger = passenger;
            this.FlightBooked = flightBooked;
            this.SeatNumber = seatNumber;
            this.SeatClass = seatClass;
            TotalPrice = CalculateTotalPrice();
        }

        public void ChangeClass(SeatClass? seatClass)
        {
            SeatClass = seatClass;
            TotalPrice = CalculateTotalPrice();
        }

        private decimal CalculateTotalPrice()
        {
            return FlightBooked.FlightBasePrice + 100 * Convert.ToDecimal(SeatClass);
        }

        public override string ToString()
        {
            return $"{this.GetHashCode()} {Passenger.PassengerName.FullName()} " +
                $"{SeatNumber} {SeatClass} {TotalPrice} {FlightBooked.FlightId} {FlightBooked.DepartureCountry} {FlightBooked.DestinationCountry}";
        }
    }
}
