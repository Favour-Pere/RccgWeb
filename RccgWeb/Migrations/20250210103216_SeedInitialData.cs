using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RccgWeb.Data;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RccgWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer("RccgConnectionStrings")
                        .Options;
            using (var context = new ApplicationDbContext(options))
            {
                int counter = 1;

                var zones = context.Zones.ToList();
                foreach (var zone in zones)
                {
                    zone.ChurchId = $"rccg-04-{counter:D4}";
                    counter++;
                }

                context.SaveChanges();

                var areas = context.Areas.ToList();
                foreach (var area in areas)
                {
                    area.ChurchId = $"rccg-04-{counter:D4}";
                    counter++;
                }
                context.SaveChanges();

                var parishes = context.Parishes.ToList();
                foreach (var parish in parishes)
                {
                    parish.ChurchId = $"rccg-04-{counter:D4}";
                    counter++;
                }
                context.SaveChanges();
            }

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "ZoneId", "ChurchId", "DateCreated", "Location", "ZoneName", "ZonePastor" },
                values: new object[,]
                {
                    { new Guid("04118129-6f06-49e6-b155-1dd856347c2a"), "TBD", new DateTime(2025, 2, 10, 10, 32, 15, 203, DateTimeKind.Utc).AddTicks(9689), "Location A", "Zone A", "Pastor John" },
                    { new Guid("eb7cce28-a62a-4b40-8b35-31fd9a13aa14"), "TBD", new DateTime(2025, 2, 10, 10, 32, 15, 204, DateTimeKind.Utc).AddTicks(313), "Location B", "Zone B", "Pastor Jane" }
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "AreaId", "AreaName", "AreaPastor", "ChurchId", "DateCreated", "Location", "ZoneId" },
                values: new object[] { new Guid("90381266-3bb9-4963-9e8b-8780ef7f2465"), "Area X", "Pastor Smith", "TBD", new DateTime(2025, 2, 10, 10, 32, 15, 205, DateTimeKind.Utc).AddTicks(5895), "Location X", new Guid("04118129-6f06-49e6-b155-1dd856347c2a") });

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "ParishId", "AreaId", "ChurchId", "DateCreated", "Location", "ParishName", "ParishPastor" },
                values: new object[] { new Guid("7e5e6bc0-3981-4cb7-b73e-a2f2a2dc3bdc"), new Guid("90381266-3bb9-4963-9e8b-8780ef7f2465"), "TBD", new DateTime(2025, 2, 10, 10, 32, 15, 206, DateTimeKind.Utc).AddTicks(2296), "Location Y", "Parish 1", "Pastor Mark" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "ParishId",
                keyValue: new Guid("7e5e6bc0-3981-4cb7-b73e-a2f2a2dc3bdc"));

            migrationBuilder.DeleteData(
                table: "Zones",
                keyColumn: "ZoneId",
                keyValue: new Guid("eb7cce28-a62a-4b40-8b35-31fd9a13aa14"));

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "AreaId",
                keyValue: new Guid("90381266-3bb9-4963-9e8b-8780ef7f2465"));

            migrationBuilder.DeleteData(
                table: "Zones",
                keyColumn: "ZoneId",
                keyValue: new Guid("04118129-6f06-49e6-b155-1dd856347c2a"));
        }
    }
}