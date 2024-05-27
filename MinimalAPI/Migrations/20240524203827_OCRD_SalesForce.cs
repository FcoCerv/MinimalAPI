using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class OCRD_SalesForce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OCRDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_CEU_CPORTAL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicTradNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_B1SYS_MainUsage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CmpPrivate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineHandles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannIBP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_NACIONALIDAD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VipDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tiktok_data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook_data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instagram_data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_RUTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Free_Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_NimboID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCRDs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OCRDs");
        }
    }
}
