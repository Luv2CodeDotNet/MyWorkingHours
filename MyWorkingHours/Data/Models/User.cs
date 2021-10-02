using System;
using System.ComponentModel.DataAnnotations;

namespace MyWorkingHours.Data.Models
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public Guid Guid { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [EmailAddress] public string EmailAdress { get; set; }
    }
}