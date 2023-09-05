using AirportTicketBookingSystem.BookingHandler;
using AirportTicketBookingSystem.FlightBoard;
using AirportTicketBookingSystem.FlightHandler;
using AirportTicketBookingSystem.ManagerHandler;
using AirportTicketBookingSystem.PassengerHandler;
using AirportTicketBookingSystem.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Channels;

namespace AirportTicketBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AirlineFlightBoard flightBoard = new AirlineFlightBoard();

            while (true)
            {
                LogInConsoleMessage();
                ChooseManagerOrPassenger ManagerOrPassenger = GetManagerOrPassenger();

                if (ManagerOrPassenger == ChooseManagerOrPassenger.IamPassenger)
                {
                    PassengerConsoleApp.RunPassengerConsole(flightBoard);
                }
                else if (ManagerOrPassenger == ChooseManagerOrPassenger.IamManager)
                {
                    ManagerConsoleApp.RunManagerConsole(flightBoard);
                }
                else if (ManagerOrPassenger == ChooseManagerOrPassenger.ExitApp)
                {
                    Console.WriteLine("Bye :)");
                    break;
                }
            }
        }

        public static void LogInConsoleMessage()
        {
            Console.WriteLine(
                "Welcome to our flight reservation system, enter the number of your position:\n" +
                "1- Passenger\n" +
                "2- Manager\n" +
                "3- Exit\n"
                );
        }

        public static ChooseManagerOrPassenger GetManagerOrPassenger()
        {
            bool isValid = true;

            var choice = Console.ReadLine();
            int optionsEnumSize = Enum.GetValues(typeof(ChooseManagerOrPassenger)).Length;

            isValid = ValidateInput.IsIntValidWithRange(choice, 1, optionsEnumSize);

            while (!isValid)
            {
                Console.WriteLine($"Input is not valid! Please enter a value between 1 and {optionsEnumSize}");
                choice = Console.ReadLine();
                isValid = ValidateInput.IsIntValidWithRange(choice, 1, optionsEnumSize);
            }

            return (ChooseManagerOrPassenger)Enum.Parse(typeof(ChooseManagerOrPassenger), choice, true);
        }
    }
}