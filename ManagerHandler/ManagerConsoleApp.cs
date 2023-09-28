using AirportTicketBookingSystem.BookingHandler;
using AirportTicketBookingSystem.FlightBoard;
using AirportTicketBookingSystem.FlightHandler;
using AirportTicketBookingSystem.PassengerHandler;
using AirportTicketBookingSystem.Validation;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.ManagerHandler
{
    public class ManagerConsoleApp
    {
        public static void RunManagerConsole(AirlineFlightBoard flightBoard)
        {
            Manager manager = GetManagerInformation();

            bool work = true;

            while (work)
            {

                ManagerConsoleText.DisplayManagerOperationsMenu();
                ManagerOptions managerOperation = GetManagerOption();

                if (managerOperation == ManagerOptions.AddFlights)
                {
                    bool isOperationSuccessful = AddingFlightsToFlightBoard(flightBoard);

                    if (isOperationSuccessful)
                        Console.WriteLine("Flights have been added successfully!");
                    else
                        Console.WriteLine("Something Went Wrong! No flights have been added");
                }
                else if (managerOperation == ManagerOptions.InfoAboutDataFormat)
                {
                    InfoAboutDataFormat();
                }
                else if (managerOperation == ManagerOptions.LookupFlight)
                {
                    List<Flight> filteredFlights = FlightBoardConsoleOperations.FilterFlights(flightBoard) ;

                    if (filteredFlights.Any())
                    {
                        FlightBoardConsoleOperations.DisplayAvailableFlights(filteredFlights);
                        
                        Console.WriteLine("Enter Flight Id");

                        int flightId = FlightBoardConsoleOperations.GetFlightIdFromUser();

                        Flight flightToBeLookedUp = flightBoard.FindFlightById(flightId) ;

                        if(flightToBeLookedUp == null)
                        {
                            Console.WriteLine("Flight was not found");
                            continue;
                        }
                        else
                        {
                            flightToBeLookedUp.GetBookings().ForEach(booking => Console.WriteLine(booking.ToString()));

                            FilterBookings(flightToBeLookedUp.GetBookings());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Such flights were not found");
                    }
                }
                else if (managerOperation == ManagerOptions.BackToMainMenu)
                {
                    work = false;
                }
            }
        }
        private static Manager GetManagerInformation()
        {
            // Implement obtaining passenger information.
            ManagerConsoleText.DisplayEnterNameWindow();

            Console.WriteLine("First Name:\n");
            string firstName = Console.ReadLine();


            Console.WriteLine("Last Name:\n");
            string lastName = Console.ReadLine();

            Manager manager = new Manager()
            {
                ManagerName = new ServiceClasses.Name(firstName, lastName)
            };

            return manager;
        }

        private static ManagerOptions GetManagerOption()
        {
            bool isValid = true;

            var choice = Console.ReadLine();
            int optionsEnumSize = Enum.GetValues(typeof(ManagerOptions)).Length;

            isValid = ValidateInput.IsIntValidWithRange(choice, 1, optionsEnumSize);

            while (!isValid)
            {
                Console.WriteLine($"Input is not valid! Please enter a value between 1 and {optionsEnumSize}");
                choice = Console.ReadLine();
                isValid = ValidateInput.IsIntValidWithRange(choice, 1, optionsEnumSize);
            }

            return (ManagerOptions)Enum.Parse(typeof(ManagerOptions), choice, true);
        }

        private static bool AddingFlightsToFlightBoard(AirlineFlightBoard flightBoard)
        {
            Console.WriteLine("Enter csv file full path:");
            var path = Console.ReadLine();

            if (!ValidateInput.IsStringValid(path))
            {
                Console.WriteLine("No..We do not accept an empty name for a file");
                return false;
            }

            if (!ManagerFilesHandler.DoesFileExist(path))
            {
                Console.WriteLine("This file path is invalid, please check for any mistakes while taking in the file");
                return false;
            }

            if (!ManagerFilesHandler.IsFileCsv(path))
            {
                Console.WriteLine("This is not a csv file!");
                return false;
            }

            List<FlightToBeValidated> flightToBeValidateds = ManagerFilesHandler.ReadDataFromCsv(path);

            List<bool> validationOfAllFlights = flightToBeValidateds.Select(flight => flight.isFlightToBeValidatedValid()).ToList();

            if (validationOfAllFlights.Contains(false))
            {
                int indexOfFalse = validationOfAllFlights.IndexOf(false) + 1;
                Console.WriteLine($"Flight at line {indexOfFalse} has problem with the input");

                return false;
            }

            List<Flight> flights = flightToBeValidateds.Select(flight => flight.ConvertToFlight()).ToList();

            List<bool> flightsElligible = flights.Select(flight => flight.IsFlightValid()).ToList();

            if (flightsElligible.Contains(false))
            {
                Flight BadFlight = flights.First(flight => !flight.IsFlightValid());
                List<ValidationResult> validationResult = BadFlight.GetFlightErrors();
                validationResult.ForEach(flight => Console.WriteLine(flight.ErrorMessage));
                return false;
            }

            flights.ForEach(flight => flightBoard.AddFlight(flight));

            return true;
        }

        private static void InfoAboutDataFormat()
        {
            Console.WriteLine("Departure Country: Required, String, 2 - 25 Characters\n");
            Console.WriteLine("Departure Airport: Required, String, 2 - 20 Characters\n");
            Console.WriteLine("Departure Time: Required,  DateTime, Format YYYY-MM-DD HH:MM:SS\n");

            Console.WriteLine("Destination Country: Required, String, 2 - 25 Characters\n");
            Console.WriteLine("Departure Airport: Required, String, 2 - 20 Characters\n");

            Console.WriteLine("Base Price: Required, Decimal, 50 - 10000$\n");

            Console.WriteLine("Number of Seats: Required, Integer, 6 - 500");

        }

        private static List<Booking> FilterBookings (List<Booking> bookings)
        {
            Console.WriteLine("Enter First Name");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Flight number");
            string flightNumberString = Console.ReadLine();

            Console.WriteLine("Enter Class");
            string seatClassString = Console.ReadLine();

            int? flightNumber = (ValidateInput.IsIntValid(flightNumberString)) ? int.Parse(flightNumberString) : null;

            SeatClass? seatClass = (ValidateInput.IsIntValidWithRange(seatClassString,0,2)) ? Enum.Parse<SeatClass>(seatClassString) : null;

            return FiterBookingsImplementation(bookings, firstName, lastName, flightNumber, seatClass);
        }

        private static List<Booking> FiterBookingsImplementation(List<Booking> bookings ,string? firstName, string? lastName, int? flightNumber, SeatClass? seatClass)
        {
            return bookings
                .Where(booking => (string.IsNullOrEmpty(firstName)) ? true : booking.Passenger.PassengerName.FirstName == firstName)
                .Where(booking => (string.IsNullOrEmpty(lastName)) ? true : booking.Passenger.PassengerName.LastName == lastName)
                .Where(booking => flightNumber == null ? true : flightNumber == booking.FlightBooked.FlightId)
                .Where(booking => seatClass == null ? true : booking.SeatClass == seatClass).ToList();
        }
    }
}
