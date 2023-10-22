using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.Contracts;
using Pokedex.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers;

[Route("/[controller]")]
public class PokemonTiposController : BaseController
{
    private readonly IPokemonTipoService _pokemonTipoService;
    public PokemonTiposController(INotificator notificator, IPokemonTipoService pokemonTipoService) : base(notificator)
    {
        _pokemonTipoService = pokemonTipoService;
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Obter os tipos", Tags = new[] { "Pokedex - Pokemón tipo" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Buscar()
    {
        return OkResponse(await _pokemonTipoService.ObterTodos());
    }
}