using RccgWeb.Data;

namespace RccgWeb.Models
{
    public class Zone
    {
        public Guid ZoneId { get; set; } = Guid.NewGuid();

        public string ChurchId { get; set; }

        public string ZoneName { get; set; }

        public string ZonePastor { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Location { get; set; }

        public ICollection<Area> Areas { get; set; }
    }
}