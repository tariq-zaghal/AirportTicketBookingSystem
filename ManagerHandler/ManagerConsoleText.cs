using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.ManagerHandler
{
    public class ManagerConsoleText
    {
        public static void DisplayEnterNameWindow()
        {
            Console.WriteLine("Enter your name:\n");
        }

        public static void DisplayManagerOperationsMenu()
        {
            Console.WriteLine("Choose an operation to perform:\n" +
                "1- Add flights to board\n" +
                "2- Info about the format of data\n" +
                "3- Look up flights\n" +
                "4- Back to main menu");
        }
    }
}
