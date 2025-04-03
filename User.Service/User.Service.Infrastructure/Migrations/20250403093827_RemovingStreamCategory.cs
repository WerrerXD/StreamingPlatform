using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingStreamCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreamCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StreamCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamCategories", x => x.Id);
                });
        }
    }
}
