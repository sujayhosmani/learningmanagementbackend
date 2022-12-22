using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagement.Infrastructure.Migrations
{
    public partial class AddAdminToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var users = new object[,] {
            {
                "sujay", "sujay@gmail.com", "password", "admin", "sujay", DateTime.UtcNow, "sujay", DateTime.UtcNow
            }};

            migrationBuilder.InsertData(
                "users", new[]
                { "name", "email", "password", "role", "created_by", "create_timestamp", "updated_by", "updated_timestamp" },
                users);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
