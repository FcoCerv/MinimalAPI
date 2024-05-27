using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class Pedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreCotizacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCaducidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtotal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListaFormulas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescPorVolumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalVentas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Impuestos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaisFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalleFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoProvinciaFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPostalFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiudadFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaisEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalleEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoProvinciaEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPostalEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiudadEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidades = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoDeProspecto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductosDeInteres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prospecto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });
        }
    }
}
