using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokedex.Infra.Migrations
{
    public partial class AddPokemonsIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "Id", "Nome", "Descricao", "PokemonTipoId", "Imagem"},
                values: new object[,]
                {
                    { 1, "Bulbasaur", "Pokemón inicial da primeira geração", 4, "../../assets/bulba.png"},
                    { 2, "Squirtle", "Pokemón inicial da primeira geração", 3, "../../assets/squirtle.png"},
                    { 3, "Charmander", "Pokemón inicial da primeira geração", 2, "../../assets/charmander.png"},
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .DeleteData("Pokemons", "Id", 1);
        }
    }
}
