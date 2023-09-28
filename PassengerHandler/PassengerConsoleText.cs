using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.PassengerHandler
{
    public class PassengerConsoleText
    {
        public static void DisplayEnterNameWindow()
        {
            Console.WriteLine("Enter your name:\n");
        }

        public static void DisplayPassengerOperationsMenu()
        {
            Console.WriteLine("Choose an operation to perform:\n" +
                                "1- Book a new flight\n" +
                                "2- Edit an existing flight\n" +
                                "3- Exit\n");
        }

        public static void PromptPassengerToChooseSeat()
        {
            Console.WriteLine("Please choose a seat from the list");
        }

        public static void PromptPassengerToChooseClass()
        {
            Console.WriteLine("What class do you want?\n" +
                "0- Economy Class\n" +
                "1- Business Class\n" +
                "2- First Class\n");
        }
    }
}
