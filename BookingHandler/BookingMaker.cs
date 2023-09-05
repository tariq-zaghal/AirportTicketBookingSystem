using AirportTicketBookingSystem.FlightHandler;
using AirportTicketBookingSystem.PassengerHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.BookingHandler
{
    public class BookingMaker
    {
        public static void MakeBooking(Passenger passenger, Booking booking, Flight flight)
        {
            passenger.AddBooking(booking);
            flight.AddBooking(booking);
        }

        public static void DeleteBooking(Passenger passenger, Booking booking, Flight flight)
        {
            passenger.RemoveBooking(booking);
            flight.RemoveBooking(booking);
        }
    }
}
