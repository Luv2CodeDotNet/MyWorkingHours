using System;
using System.ComponentModel.DataAnnotations;

namespace MyWorkingHours.Data.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string firstName, string lastName, string emailAdress)
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            EmailAdress = emailAdress;
            EnvironmentName = Environment.UserName;
            MachineName = Environment.MachineName;
        }

        [Key] public int Id { get; set; }
        [Required] public Guid Guid { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [EmailAddress] public string EmailAdress { get; set; }
        [Required] public string EnvironmentName { get; set; }
        [Required] public string MachineName { get; set; }
    }
}