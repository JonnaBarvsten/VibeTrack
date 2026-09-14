using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdAndDateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_Date",
                table: "DailyLogs",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DailyLogs_Date",
                table: "DailyLogs");
        }
    }
}
