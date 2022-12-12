using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations
{
    /// <inheritdoc />
    public partial class SeededCorrectUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Name", "Password" },
                values: new object[] { "user", "pKj8ZhaWCE7Qed6qJOrYoA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Micke", "YlLIEhPWqLis4l23Lcbx3w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Micke", "YlLIEhPWqLis4l23Lcbx3w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Makkan", "6YUUO+lD8+IYI/0UPG4mvg==" });
        }
    }
}
