using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiStarPare.Migrations
{
    public partial class Initialsqlserver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    TotalPassageiros = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estacionamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime", nullable: true),
                    CarroId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estacionamentos_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamentos_CarroId",
                table: "Estacionamentos",
                column: "CarroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estacionamentos");

            migrationBuilder.DropTable(
                name: "Carros");
        }
    }
}
