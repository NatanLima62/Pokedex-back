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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarPokemonDto dto)
    {
        return OkResponse(await _pokemonService.Adicionar(dto));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Obter um pokemón por id", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _pokemonService.ObterPorId(id));
    }
    
    [HttpGet("nome/{nome}")]
    [SwaggerOperation(Summary = "Obter um pokemón por nome", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorNome(string nome)
    {
        return OkResponse(await _pokemonService.ObterPorNome(nome));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar pokemóns", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Buscar()
    {
        return OkResponse(await _pokemonService.Buscar());
    }
    
    [HttpGet("tipo/{id:int}")]
    [SwaggerOperation(Summary = "Buscar pokemóns por tipo", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorTipo(int id)
    {
        return OkResponse(await _pokemonService.BuscarPorTipo(id));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar um pokemón", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] AtualizarPokemonDto dto)
    {
        return OkResponse(await _pokemonService.Atualizar(id, dto));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Remover um pokemón", Tags = new[] { "Pokedex - Pokemón" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Remover(int id)
    {
        await _pokemonService.Remover(id);
        return NoContentResponse();
    }
}