using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.ServiceClasses
{
    public class Name
    {
        [Required(ErrorMessage = "You must add a valid first name")]
        [StringLength(50, ErrorMessage = "Your first name should be less than 50 characters (or else you're probably some kind of viruse,\n we do not welcome viruses on our planes)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must add a valid last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName; 
        }

        public override string ToString()
        {
            return $"First name = {FirstName}\n" +
                   $"Last name = {LastName}\n";
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}\n";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if(!(obj is Name))
                return false;

            return this.FirstName == ((Name)obj).FirstName
                && this.LastName == ((Name)obj).LastName;
        }
    }
}
