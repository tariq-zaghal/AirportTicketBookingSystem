# Airport Ticket Booking System

This is a console [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/) project that I have completed while on an internship with FootHill Technologies.
The Project mimics a flight reservation system than an airline would use To allow passengers to book flights and flight conductors to upload them.

## Introduction

The purpose of this project is to play the role of a visual midiator between a manager and a passenger.
It allows the potential passenger of an Airline to checkout the available flights and book and book one with his favorite seat and desired class.

Moreover, the project permits flight managers to upload a bulk of flights using [.CSV](https://en.wikipedia.org/wiki/Comma-separated_values), the project also tests the inputs and returns detailed explanation of errors if existed.


## Getting Started

Before you begin, please make sure you have the following prerequisites:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (version 6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) (optional)
- [CsvHelper](https://joshclose.github.io/CsvHelper/) (version 30.0)

Then, Clone the repository to your local machine

```

git clone https://github.com/tariq-zaghal/AirportTicketBookingSystem.git

```

After that, you can navigate to the project in your own device (I don't know any better but I think it would be safer to use Visual Studio since I developed the project using it).

If you, however, want to run it without an IDE, you can do so by:

  1- Navigate to the project directory
  
```

cd AirportTicketBookingSystem

```
  2- Build and Run Project

```

dotnet build
dotnet run

```  

## How to use Booking System?

After you run the project, you will get prompted with the following menu: 

![Main menu](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/main%20menu.png)

As you can see, there is a menu that contains logging in as a passenger, manager, or exiting the whole thing!

You can choose any of them by typing in the number of the operation.

Let's explore all the options one by one!

### Passenger

- When you choose to log in as a passenger you'll be prompted with a log in screen where you would enter your name (first and last).

![Enter passenger info prompt](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/passengerFullNameMenu.png)

After you enter your name, the program makes a Passenger opject to hold your name and your bookings.

- After you input your name, a menu that contains the operations you can perform appears:

![Passenger options](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/passengerOptionsMenu.png)

You can either choose to book a new flight form a list of flights or edit an existing one.

- Say you have chosen to book a new flight, you will be directed to a filter that would help you narrow down your choices:

![List of flightd](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/PassengerFlightFilter.png)

As you can see there are many options to let you limit your choices successfully, you can filter the flights 

using Departure place, ddestination, or even specifying ranges for price or date and time.

Note that if you leave the filter empty it will not implement any filtering regarding that field. In the example above, all the flights available get displayed since all the fields are empty.

- After filtering the flights, you might see a list like the one below:

![List of flights](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/PassengerMenuOfFlights.png)

After you get prompt with this list you need to choose a flight from them by typing in the fligt ID (first number on the left), then choose a seat from the flight and then a class and that way you're done with the booking part.

- now let's say you have chosen to edit a booking, you will get prompted with a list of all your bookings:

![List of bookings](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/PassengerEditBookingList.png)

the fields are: booking code, seat number, seat class, price, flight ID, departure counrty, destination country.

you can choose the booking you want to edit by typing in the booking code and then you will see a screen tht contains seats to choose from and later a class.


### Manager

The operations of the manager are somewhat similar except for the task of adding flights.

After you choose to log in as manager you get in the same log in screen as that of the passenger in which you enter your first and last name.

then you will see this menu:

![Manager main menu](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/ManagerMainMenu.png)

There are three main operations here, let's dive into them one by one!

- Arguably the most important operation of the bunch is the first one "Add flights to board".

This option lets the manager upload a .CSV file (Comma Seperated Values) containing the details of all the flights he wants to add to the list of flights the user books his flight from.

![Manager csv file](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/ManagerAddFile.png)

Notice that we add the whole path of the file. Be careful! the file MUST be of type csv, otherwise it will not upload.

If know error was found in the file whether in validity of input format or validity of data itself the program will print a success message.

It is important to pay attention to the date format when creating the csv file, it should be written in this format `yyyy-MM-dd HH:mm:dd`, otherwise a parsing error will be returned

here is an example of a valid data input:

```
Departure Country,Departure Time,Departure Airport,Destination Country,Destination Airport,Base Price,Number of Seats,
Canada,2023-10-20 18:20:00,Vancoover,Germany,Frankfurt,2499,320
Canada,2023-09-10 13:25:00,Ontario,Belgium,Brussels,1350,200
USA,2023-09-12 06:40:00,LAX,Canada,Vancoover,999,10
USA,2023-11-01 07:30:00,Kennedy,UAE,Abu Dhabi,5490,330
Turkey,2023-10-05 19:00:00,Isntabul,Jordan,Alia,500,170
Turkey,2023-09-21 20:45:00,Sabiha Gokcen,France,Charles de Gaulle,140,150
Jordan,2024-01-23 01:10:00,Alia,KSA,King Fahd,220,320
Jordan,2024-02-02 02:20:00,Marka,Egypt,Cairo,320,100
```

You can use this example as a template for your future files, you're welcome!!!

- For the second option, it simply prints information about the data format and information about the different fields.

Here is its output!!

![Format data](https://github.com/tariq-zaghal/AirportTicketBookingSystem/blob/dev/ScreenshotsForFlightTicketReadme/ManagerInfoAboutFormat.png)

- The third option in the list is very similar to the filter of the passenger. However, you can later look into the bookings inside a certain flight, that's all!!

Hope you find it useful!
