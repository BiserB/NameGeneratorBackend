using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.Data.Migrations
{
    public partial class AddNameAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaleNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Record = table.Column<string>(maxLength: 256, nullable: false),
                    Popularity = table.Column<decimal>(nullable: false),
                    NameTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaleNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaleNames_NameTypes_NameTypeId",
                        column: x => x.NameTypeId,
                        principalTable: "NameTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaleNames_NameTypeId",
                table: "MaleNames",
                column: "NameTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaleNames");

            migrationBuilder.DropTable(
                name: "NameTypes");
        }
    }
}
