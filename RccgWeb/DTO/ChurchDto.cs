using RccgWeb.Enum;
using RccgWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.DTO
{
    public class ChurchDto(Church church)
    {
        public string ID { get; set; } = church.ID.ToString();

        public string ChurchName { get; set; } = church.ChurchName;

        public string ChurchLocation { get; set; } = church.ChurchLocation;

        public ChurchType ChurchType { get; set; } = church.ChurchType;
    }
}