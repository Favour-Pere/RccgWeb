using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;

namespace RccgWeb
{
    public static class ChurchIdGenerator
    {
        public static string GenerateChurchId(ApplicationDbContext context)
        {
            var lastChurchId = context.Zones.Select(z => z.ChurchId)
                                      .Union(context.Areas.Select(a => a.ChurchId))
                                      .Union(context.Parishes.Select(p => p.ChurchId))
                                      .OrderByDescending(id => id)
                                      .FirstOrDefault();
            if (lastChurchId == null)
            {
                return "rccg-04-0001";
            }

            var lastNumber = int.Parse(lastChurchId.Split('-').Last());

            return $"rccg-04-{(lastNumber + 1).ToString("D4")}";
        }
    }
}