using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeetingManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "MeetingItemStatusID", "ActionRequired", "MeetingItemID", "ResponsiblePerson", "StatusDescription" },
                values: new object[,]
                {
                    { 11, "Varushka Singh", 15, "V S", "Open" },
                    { 12, "Completed the task", 16, "V S", "Closed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "MeetingItemStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "MeetingItemStatusID",
                keyValue: 2);
        }
    }
}
