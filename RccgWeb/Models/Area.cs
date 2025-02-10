﻿using RccgWeb.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RccgWeb.Models
{
    public class Area
    {
        [Key]
        public Guid AreaId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(20)]
        public string ChurchId { get; set; }

        [Required]
        [StringLength(100)]
        public string AreaName { get; set; }

        [Required]
        [StringLength(100)]
        public string AreaPastor { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [ForeignKey("Zone")]
        public Guid ZoneId { get; set; }

        [JsonIgnore]
        public Zone Zone { get; set; }

        public ICollection<Parish> Parishes { get; set; }
    }
}