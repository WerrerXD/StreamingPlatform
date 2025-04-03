using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRole_Rolez_RolesId",
                table: "AppUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rolez",
                table: "Rolez");

            migrationBuilder.RenameTable(
                name: "Rolez",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRole_Roles_RolesId",
                table: "AppUserRole",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRole_Roles_RolesId",
                table: "AppUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rolez");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rolez",
                table: "Rolez",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRole_Rolez_RolesId",
                table: "AppUserRole",
                column: "RolesId",
                principalTable: "Rolez",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
