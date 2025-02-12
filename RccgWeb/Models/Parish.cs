using RccgWeb.Data;
using System.Text.Json.Serialization;

namespace RccgWeb.Models
{
    public class Parish
    {
        public Guid ParishId { get; set; } = Guid.NewGuid();

        public string ChurchId { get; set; }

        public string ParishName { get; set; }

        public string ParishPastor { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Location { get; set; }

        public Guid AreaId { get; set; }

        [JsonIgnore]
        public Area Area { get; set; }
    }
}