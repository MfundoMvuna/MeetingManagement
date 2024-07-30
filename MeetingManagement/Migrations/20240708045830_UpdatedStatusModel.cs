using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeetingManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Status_MeetingItemID",
                table: "Status");

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "MeetingItemStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "MeetingItemStatusID",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Status_MeetingItemID",
                table: "Status",
                column: "MeetingItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Status_MeetingItemID",
                table: "Status");

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "MeetingItemStatusID", "ActionRequired", "MeetingItemID", "ResponsiblePerson", "StatusDescription" },
                values: new object[,]
                {
                    { 1, "None", 1, "Manager", "Open" },
                    { 2, "Complete the task", 2, "Manager", "Closed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_MeetingItemID",
                table: "Status",
                column: "MeetingItemID",
                unique: true);
        }
    }
}
