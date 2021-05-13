using Microsoft.EntityFrameworkCore.Migrations;

namespace CSUBWorkFlowProject.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isManager = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logins", x => x.username);
                });

            migrationBuilder.InsertData(
                table: "logins",
                columns: new[] { "username", "isManager", "password" },
                values: new object[] { "auser", false, "testuser" });

            migrationBuilder.InsertData(
                table: "logins",
                columns: new[] { "username", "isManager", "password" },
                values: new object[] { "amanager", true, "testmanager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logins");
        }
    }
}
