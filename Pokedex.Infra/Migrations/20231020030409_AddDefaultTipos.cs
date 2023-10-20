using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokedex.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PokemonTipos",
                columns: new[] { "Id", "Nome"},
                values: new object[,]
                {
                    { 1, "Normal"},
                    { 2, "Fogo"},
                    { 3, "Água"},
                    { 4, "Grama"},
                    { 5, "Voador"},
                    { 6, "Lutador"},
                    { 7, "Veneno"},
                    { 8, "Elétrico"},
                    { 9, "Terra"},
                    { 10, "Pedra"},
                    { 11, "Psíquico"},
                    { 12, "Gelo"},
                    { 13, "Inseto"},
                    { 14, "Fantasma"},
                    { 15, "Ferro"},
                    { 16, "Dragão"},
                    { 17, "Sombrio"},
                    { 18, "Fada"},
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .DeleteData("PokemonTipos", "Id", 1);
        }
    }
}
