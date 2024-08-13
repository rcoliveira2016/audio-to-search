using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioToSearch.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTranscricaoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogarAudioTranscricaoEntity",
                columns: table => new
                {
                    UId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Texto = table.Column<string>(type: "TEXT", nullable: false),
                    Inicio = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Final = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    UIdCatalogarAudio = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogarAudioTranscricaoEntity", x => x.UId);
                    table.ForeignKey(
                        name: "FK_CatalogarAudioTranscricaoEntity_CatalogarAudioEntity_UIdCatalogarAudio",
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
        }
    }
}
