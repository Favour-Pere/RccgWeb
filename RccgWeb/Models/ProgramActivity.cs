using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RccgWeb.Models
{
    public class ProgramActivity
    {
        [Key]
        public Guid ProgramActivityId { get; set; }

        [Required]
        public string ActivityName { get; set; } = string.Empty;

        [Required]
        public string ActivityDescription { get; set; } = string.Empty;

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateTimeSubmitted { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Offering { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tithe { get; set; }

        [Required]
        public int Attendance { get; set; }

        [Required]
        public int ActiveWorkers { get; set; }

        [Required]
        public string PastorInCharge { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string ChurchId { get; set; } = string.Empty;
    }
}