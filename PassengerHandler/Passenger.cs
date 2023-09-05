using AirportTicketBookingSystem.BookingHandler;
using AirportTicketBookingSystem.ServiceClasses;

namespace AirportTicketBookingSystem.PassengerHandler
{
    public class Passenger
    {
        public Name PassengerName { get; set; }

        public List<Booking> PassengerBookings { get; set; }

        public Passenger()
        {
            PassengerBookings = new List<Booking>();
        }

        public void AddBooking(Booking booking)
        {
            PassengerBookings.Add(booking);
        }

        public void RemoveBooking(Booking booking)
        {
            PassengerBookings.Remove(booking);
        }
    }
}
