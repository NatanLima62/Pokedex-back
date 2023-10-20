using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.V1.Pokemon;
using Pokedex.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers;

[Route("/[controller]")]
public class PokemonsController : BaseController
{
    private readonly IPokemonService _pokemonService;

    public PokemonsController(INotificator notificator, IPokemonService pokemonService) : base(notificator)
    {
        _pokemonService = pokemonService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cadastro de um pokemón", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(typeof(PokemonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarPokemonDto dto)
    {
        return OkResponse(await _pokemonService.Adicionar(dto));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter um podemón", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _pokemonService.ObterPorId(id));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar podemóns", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Buscar([FromQuery] BuscarPokemonDto dto)
    {
        return OkResponse(await _pokemonService.Buscar(dto));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar um pokemón", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(int id, [FromBody] AtualizarPokemonDto dto)
    {
        return OkResponse(await _pokemonService.Atualizar(id, dto));
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Remover um pokemón", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Remover(int id)
    {
        await _pokemonService.Remover(id);
        return NoContentResponse();
    }
}