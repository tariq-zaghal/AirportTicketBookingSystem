using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightHandler;
using CsvHelper;

namespace AirportTicketBookingSystem.ManagerHandler
{
    public class ManagerFilesHandler
    {
        public static bool DoesFileExist(string path)
        {
            return File.Exists(path);
        }

        public static bool IsFileCsv(string path)
        {
            return path.EndsWith(".csv");
        }

        public static List<FlightToBeValidated> ReadDataFromCsv(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<FlightToBeValidated>().ToList();
                    return records;
                }
            }
        }
    }
}
