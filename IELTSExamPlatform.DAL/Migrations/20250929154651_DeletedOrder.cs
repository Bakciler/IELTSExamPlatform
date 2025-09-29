using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IELTSExamPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeletedOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Blanks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Blanks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
