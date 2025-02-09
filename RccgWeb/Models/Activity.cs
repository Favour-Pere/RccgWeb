using System.ComponentModel.DataAnnotations;

namespace RccgWeb.Models
{
    public class Activity
    {
        [Key]
        public Guid ActivityId { get; set; }

        public string ActivityName { get; set; }

        public DateOnly DateCreated { get; set; }

        public DateTime DateTimeSubmitted { get; set; } = DateTime.Now;

        public double Offering { get; set; }

        public double Tithe { get; set; }

        public int Attendance { get; set; }

        public int ActiveWorkers { get; set; }

        public string PastorInCharge { get; set; }
    }
}