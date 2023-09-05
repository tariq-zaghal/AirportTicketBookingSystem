using AirportTicketBookingSystem.BookingHandler;
using AirportTicketBookingSystem.FlightBoard;
using AirportTicketBookingSystem.FlightHandler;
using AirportTicketBookingSystem.ManagerHandler;
using AirportTicketBookingSystem.Validation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace AirportTicketBookingSystem.PassengerHandler
{
    public class PassengerConsoleApp
    {
        public static void RunPassengerConsole(AirlineFlightBoard flightBoard)
        {
            Passenger passenger = GetPassengerInformation();

            bool work = true;

            while (work)
            {
                PassengerConsoleText.DisplayPassengerOperationsMenu();

                PassengerOptions passengerOption = GetPassengerOption();

                if (passengerOption == PassengerOptions.BookFlight)
                {
                    BookFlightOperation(passenger,  flightBoard);
                }
                else if (passengerOption == PassengerOptions.EditABooking)
                {
                    EditABookingOperation(passenger);
                }
                else
                {
                    Console.WriteLine("Exiting Passenger Screen...");
                    work = false;
                }
            }
        }

        private static Passenger GetPassengerInformation()
        {
            // Implement obtaining passenger information.
            PassengerConsoleText.DisplayEnterNameWindow();

            Console.WriteLine("First Name:\n");
            string firstName = Console.ReadLine();


            Console.WriteLine("Last Name:\n");
            string lastName = Console.ReadLine();

            Passenger passenger = new Passenger()
            {
                PassengerName = new ServiceClasses.Name(firstName, lastName)
            };

            return passenger;
        }

        private static PassengerOptions GetPassengerOption()
        {
            bool isValid = true;

            var choice = Console.ReadLine();
            int optionsEnumSize = Enum.GetValues(typeof(PassengerOptions)).Length;

            isValid = ValidateInput.IsIntValidWithRange(choice, 1, optionsEnumSize);

            while (!isValid)
            {
                Console.WriteLine($"Input is not valid! Please enter a value between 1 and {optionsEnumSize}");
                choice = Console.ReadLine();
                isValid = ValidateInput.IsIntValidWithRange(choice, 1, optionsEnumSize);
            }

            return (PassengerOptions)Enum.Parse(typeof(PassengerOptions), choice, true);
        }

        private static SeatClass? GetPassengerSeatClassFromUser()
        {
            PassengerConsoleText.PromptPassengerToChooseClass();

            SeatClass? seatClass = null;
            string seatClassToBeParsed = Console.ReadLine();
            if (seatClassToBeParsed == "0" || seatClassToBeParsed == "1" || seatClassToBeParsed == "2")
                seatClass = GetPassengerSeatClass(seatClassToBeParsed);

            return seatClass;
        }

        private static void BookFlightOperation(Passenger passenger,AirlineFlightBoard flightBoard)
        {
            List<Flight> filteredFlights = FlightBoardConsoleOperations.FilterAvailableFlights(flightBoard);

            if (filteredFlights.Any())
            {
                FlightBoardConsoleOperations.DisplayAvailableFlights(filteredFlights);

                Console.WriteLine("Enter the Id of the flight you want to book: \n");
                int flightId = FlightBoardConsoleOperations.GetFlightIdFromUser();
                if (flightId == -1) 
                {
                    Console.WriteLine("Invalid input");
                    return;
                }

                Flight? flightToBook = flightBoard.FindFlightById(flightId);

                if (flightToBook != null)
                {
                    int chosenSeat = ChooseSeat(flightToBook);
                    if (chosenSeat == -1)
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    if (flightToBook.ReserveSpecificSeat(chosenSeat) != -1)
                    {
                        SeatClass? seatClass = GetPassengerSeatClassFromUser();

                        Booking newBooking = new Booking(passenger, flightToBook, chosenSeat, seatClass);
                        BookingMaker.MakeBooking(passenger, newBooking, flightToBook);
                        Console.WriteLine(newBooking.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Seat was not found");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Flight with such specifications was not found\n");
                return;
            }
        }

        private static void EditABookingOperation(Passenger passenger)
        {
            Booking? bookingToEdit = ChooseBookingToEdit(passenger);

            if (bookingToEdit != null)
            {
                int chosenNewSeat = ChooseSeat(bookingToEdit.FlightBooked);
                bookingToEdit.FlightBooked.UnReserveSeat(bookingToEdit.SeatNumber);
                bookingToEdit.SeatNumber = chosenNewSeat;

                SeatClass? seatClass = GetPassengerSeatClassFromUser();
                if (seatClass != null)
                    bookingToEdit.ChangeClass(seatClass);
            }
            else
            {
                Console.WriteLine("Invalid input!");
                return;
            }

        }

        private static int ChooseSeat(Flight flightToBook)
        {
            List<int> availableSeats = flightToBook.getSeats();

            PassengerConsoleText.PromptPassengerToChooseSeat();

            flightToBook.getSeats().ForEach(seat => Console.WriteLine(seat));
            int chosenSeat = -1;
            var chosenSeatInput = Console.ReadLine();
            if (ValidateInput.IsIntValid(chosenSeatInput))
                chosenSeat = int.Parse(chosenSeatInput);

            return chosenSeat;
        }

        private static SeatClass? GetPassengerSeatClass(string classToBeParsed) 
        {
            return (SeatClass)Enum.Parse(typeof(SeatClass), classToBeParsed);
        }

        private static Booking? ChooseBookingToEdit(Passenger passenger)
        {
            DisplayPassengerBookings(passenger);

            Console.WriteLine("Choose Booking From List By typing in its Code");

            var bookingInput = Console.ReadLine();
            int bookingCode = 0;

            if (ValidateInput.IsIntValid(bookingInput))
                bookingCode = int.Parse(bookingInput);

            Booking? BookingToEdit = passenger.PassengerBookings.Find(booking => booking.GetHashCode() == bookingCode);

            return BookingToEdit;
        }

        private static void DisplayPassengerBookings(Passenger passenger)
        {
            passenger.PassengerBookings.ForEach(booking => Console.WriteLine(booking.ToString()));
        }
    }
}
