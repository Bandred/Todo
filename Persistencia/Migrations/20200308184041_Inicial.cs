using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistradoAt = table.Column<DateTime>(nullable: false),
                    ActualizadoAt = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistradoAt = table.Column<DateTime>(nullable: false),
                    ActualizadoAt = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    EpsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Eps_EpsId",
                        column: x => x.EpsId,
                        principalTable: "Eps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistradoAt = table.Column<DateTime>(nullable: false),
                    ActualizadoAt = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UsuarioId",
                table: "Todos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EpsId",
                table: "Usuarios",
                column: "EpsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Eps");
        }
    }
}
