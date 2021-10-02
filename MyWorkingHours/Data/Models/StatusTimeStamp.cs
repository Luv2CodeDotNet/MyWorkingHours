using System;
using System.ComponentModel.DataAnnotations;

namespace MyWorkingHours.Data.Models
{
    public class StatusTimeStamp
    {
        [Key] public int Id { get; set; }
        [Required] public Guid Guid { get; set; }
        [Required] public DateTime TimeStamp { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string MachineName { get; set; }
        [Required] public bool ScreenLocked { get; set; }
    }
}