using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Services.Interfaces;

namespace RccgWeb.Services
{
    public class ChurchService : IChurchService
    {
        private readonly ApplicationDbContext _context;

        public ChurchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetChurchNameAsync(string churchId)
        {
            var zone = await _context.Zones.FirstOrDefaultAsync(z => z.ChurchId == churchId);
            if (zone != null)
                return zone.ZoneName;

            var area = await _context.Areas.FirstOrDefaultAsync(a => a.ChurchId == a.ChurchId);
            if (area != null) return area.AreaName;

            var parish = await _context.Parishes.FirstOrDefaultAsync(p => p.ChurchId == p.ChurchId);
            if (parish != null) return parish.ParishName;

            return null;
        }
    }
}