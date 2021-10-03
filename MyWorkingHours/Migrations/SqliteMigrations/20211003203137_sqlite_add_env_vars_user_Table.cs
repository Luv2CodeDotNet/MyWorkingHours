using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkingHours.Migrations.SqliteMigrations
{
    public partial class sqlite_add_env_vars_user_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnvironmentName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnvironmentName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "Users");
        }
    }
}
