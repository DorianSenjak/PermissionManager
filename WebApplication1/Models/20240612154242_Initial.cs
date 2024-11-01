using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Models
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__App__8E2CF7D90C92F622", x => x.AppID);
                });

            migrationBuilder.CreateTable(
                name: "Baza",
                columns: table => new
                {
                    BazaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BazaName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Baza__BEC41B4FECB7A9DD", x => x.BazaID);
                });

            migrationBuilder.CreateTable(
                name: "Okolina",
                columns: table => new
                {
                    OKLID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OKLName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Okolina__76145ED8FCC647A2", x => x.OKLID);
                });

            migrationBuilder.CreateTable(
                name: "App_Baza",
                columns: table => new
                {
                    IDApp = table.Column<int>(type: "int", nullable: true),
                    IDBaza = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__App_Baza__IDApp__4E88ABD4",
                        column: x => x.IDApp,
                        principalTable: "App",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK__App_Baza__IDBaza__4F7CD00D",
                        column: x => x.IDBaza,
                        principalTable: "Baza",
                        principalColumn: "BazaID");
                });

            migrationBuilder.CreateTable(
                name: "App_Baza_Okolina",
                columns: table => new
                {
                    IDApp = table.Column<int>(type: "int", nullable: true),
                    IDBaza = table.Column<int>(type: "int", nullable: true),
                    IDOKL = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__App_Baza___IDApp__5FB337D6",
                        column: x => x.IDApp,
                        principalTable: "App",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK__App_Baza___IDBaz__60A75C0F",
                        column: x => x.IDBaza,
                        principalTable: "Baza",
                        principalColumn: "BazaID");
                    table.ForeignKey(
                        name: "FK__App_Baza___IDOKL__619B8048",
                        column: x => x.IDOKL,
                        principalTable: "Okolina",
                        principalColumn: "OKLID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_Baza_IDApp",
                table: "App_Baza",
                column: "IDApp");

            migrationBuilder.CreateIndex(
                name: "IX_App_Baza_IDBaza",
                table: "App_Baza",
                column: "IDBaza");

            migrationBuilder.CreateIndex(
                name: "IX_App_Baza_Okolina_IDApp",
                table: "App_Baza_Okolina",
                column: "IDApp");

            migrationBuilder.CreateIndex(
                name: "IX_App_Baza_Okolina_IDBaza",
                table: "App_Baza_Okolina",
                column: "IDBaza");

            migrationBuilder.CreateIndex(
                name: "IX_App_Baza_Okolina_IDOKL",
                table: "App_Baza_Okolina",
                column: "IDOKL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_Baza");

            migrationBuilder.DropTable(
                name: "App_Baza_Okolina");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "Baza");

            migrationBuilder.DropTable(
                name: "Okolina");
        }
    }
}
