using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Username");
        }
    }
}
