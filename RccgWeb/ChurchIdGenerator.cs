using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;

namespace RccgWeb
{
    public static class ChurchIdGenerator
    {
        public static string GenerateChurchId<T>(ApplicationDbContext context, string prefix = "rccg-04-") where T : class
        {
            var lastChurchId = context.Set<T>().OrderByDescending(e => EF.Property<string>(e, "ChurchId"))
                .Select(e => EF.Property<string>(e, "ChurchId")).FirstOrDefault();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastChurchId) && lastChurchId.StartsWith(prefix))
            {
                string numberPart = lastChurchId.Replace(prefix, "");

                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{nextNumber:D4}";
        }
    }
}