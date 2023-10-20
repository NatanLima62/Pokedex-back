using Pokedex.Application.Dtos.V1.Base;

namespace Pokedex.Application.Dtos.V1.Pokemon;

public class BuscarPokemonDto : BuscaPaginadaDto<Domain.Entities.Pokemon>
{
    public string? Nome { get; set; }
    public int? PokemonTipo { get; set; }
    
    public override void AplicarFiltro(ref IQueryable<Domain.Entities.Pokemon> query)
    {
        if (!string.IsNullOrWhiteSpace(Nome))
        {
            query = query.Where(c => c.Nome.Contains(Nome));
        }

        if (PokemonTipo is > 0)
        {
            query = query.Where(c=> c.PokemonTipoId == PokemonTipo);
        }
    }
    
    public override void AplicarOrdenacao(ref IQueryable<Domain.Entities.Pokemon> query)
    {
        if (DirecaoOrdenacao.ToLower().Equals("desc"))
        {
            query = OrdenarPor switch
            {
                "Id" => query.OrderByDescending(c => c.Id),
                "PokemonTipo.Nome" => query.OrderByDescending(c => c.PokemonTipo.Nome),
                "Nome" or _ => query.OrderByDescending(c => c.Nome)
            };
            return;
        }

        query = OrdenarPor switch
        {
            "Id" => query.OrderBy(c => c.Id),
            "CursoTipo.Nome" => query.OrderBy(c => c.PokemonTipo.Nome),
            "Nome" or _ => query.OrderBy(c => c.Nome)
        };
    }
}