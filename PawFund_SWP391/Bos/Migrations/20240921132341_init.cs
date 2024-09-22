using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bos.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: true),
                    TotalDonate = table.Column<double>(type: "double", nullable: true),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.AccountID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
