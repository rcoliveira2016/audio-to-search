using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace AudioToSearch.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "CatalogarAudioEntity",
                columns: table => new
                {
                    UId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogarAudioEntity", x => x.UId);
                });

            migrationBuilder.CreateTable(
                name: "CatalogarAudioTranscricaoEntity",
                columns: table => new
                {
                    UId = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    Inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Final = table.Column<TimeSpan>(type: "interval", nullable: false),
                    UIdCatalogarAudio = table.Column<Guid>(type: "uuid", nullable: false),
                    Embedding = table.Column<Vector>(type: "vector(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogarAudioTranscricaoEntity", x => x.UId);
                    table.ForeignKey(
                        name: "FK_CatalogarAudioTranscricaoEntity_CatalogarAudioEntity_UIdCat~",
                        column: x => x.UIdCatalogarAudio,
                        principalTable: "CatalogarAudioEntity",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogarAudioTranscricaoEntity_UIdCatalogarAudio",
                table: "CatalogarAudioTranscricaoEntity",
                column: "UIdCatalogarAudio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogarAudioTranscricaoEntity");

            migrationBuilder.DropTable(
                name: "CatalogarAudioEntity");
        }
    }
}
