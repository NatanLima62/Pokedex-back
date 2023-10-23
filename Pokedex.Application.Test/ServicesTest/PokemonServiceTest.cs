using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation.Results;
using Moq;
using Pokedex.Application.Dtos.V1.Pokemon;
using Pokedex.Application.Services;
using Pokedex.Application.Test.Fixtures;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Domain.Entities;
using Xunit;

namespace Pokedex.Application.Test.ServicesTest;

public class PokemonServiceTest : BaseServiceTest, IClassFixture<ServicesFixture>
{
    private readonly PokemonService _pokemonService;
    private readonly Mock<IPokemonRepository> _pokemonRepositoryMock = new();

    public PokemonServiceTest(ServicesFixture fixture)
    {
        _pokemonService = new PokemonService(fixture.Mapper, NotificatorMock.Object, _pokemonRepositoryMock.Object);
    }

    #region Adicionar

    [Fact]
    public async Task Adicionar_PokemonInvalido_HandleErros()
    {
        SetupMocks();

        var dto = new AdicionarPokemonDto
        {
            Nome = "",
            Descricao = "",
            PokemonTipoId = 0
        };

        var pokemon = await _pokemonService.Adicionar(dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Once);
            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<string>()), Times.Once);

            _pokemonRepositoryMock.Verify(c => c.Adicionar(It.IsAny<Pokemon>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Never);
        }
    }

    [Fact]
    public async Task Adicionar_PokemonExistente_Handle()
    {
        SetupMocks(true);

        var dto = new AdicionarPokemonDto
        {
            Nome = "Pokemon",
            Descricao = "Descricao",
            PokemonTipoId = 1
        };

        var pokemon = await _pokemonService.Adicionar(dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<string>()), Times.Once);

            _pokemonRepositoryMock.Verify(c => c.Adicionar(It.IsAny<Pokemon>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Never);
        }
    }

    [Fact]
    public async Task Adicionar_ErroAoSalvar_Handle()
    {
        SetupMocks();

        var dto = new AdicionarPokemonDto
        {
            Nome = "Pokemon",
            Descricao = "Descricao",
            PokemonTipoId = 1
        };

        var pokemon = await _pokemonService.Adicionar(dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<string>()), Times.Once);

            _pokemonRepositoryMock.Verify(c => c.Adicionar(It.IsAny<Pokemon>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Once);
        }
    }

    [Fact]
    public async Task Adicionar_Pokemon_Sucesso()
    {
        SetupMocks(commit: true);

        var dto = new AdicionarPokemonDto
        {
            Nome = "Pokemon",
            Descricao = "Descricao",
            PokemonTipoId = 1
        };

        var pokemon = await _pokemonService.Adicionar(dto);

        using (new AssertionScope())
        {
            pokemon.Should().NotBeNull();

            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            NotificatorMock
                .Verify(c => c.Handle(It.IsAny<string>()), Times.Never);

            _pokemonRepositoryMock.Verify(c => c.Adicionar(It.IsAny<Pokemon>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Once);
        }
    }

    #endregion

    #region ObterPorId

    [Fact]
    public async Task ObterPorId_PokemonInexistente_HandleNotFounrResourse()
    {
        SetupMocks();

        const int id = 2;
        var pokemon = await _pokemonService.ObterPorId(id);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();
            NotificatorMock.Verify(c => c.HandleNotFoundResourse(), Times.Once);
        }
    }

    [Fact]
    public async Task ObterPorId_PokemonExistente_Sucesso()
    {
        SetupMocks();

        const int id = 1;
        var pokemon = await _pokemonService.ObterPorId(id);

        using (new AssertionScope())
        {
            pokemon.Should().NotBeNull();
            pokemon.Should().BeOfType<PokemonDto>();
            NotificatorMock.Verify(c => c.HandleNotFoundResourse(), Times.Never);
        }
    }

    #endregion

    #region ObterTodos

    [Fact]
    public async Task ObterTodos_PokemonsInexistente_Handle()
    {
        SetupMocks(possuiPokemons: false);

        var pokemons = await _pokemonService.ObterTodos();

        using (new AssertionScope())
        {
            pokemons.Should().BeEmpty();
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
        }
    }

    [Fact]
    public async Task ObterTodos_PokemonsExistente_Sucesso()
    {
        SetupMocks(possuiPokemons: true);

        var pokemons = await _pokemonService.ObterTodos();

        using (new AssertionScope())
        {
            pokemons.Should().NotBeEmpty();
            pokemons.Should().BeOfType<List<PokemonDto>>();
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
        }
    }

    #endregion

    #region Buscas

    [Fact]
    public async Task Buscar_PokemonsInexistente_Handle()
    {
        SetupMocks(possuiPokemons: false);

        const string nome = "Pokemon";
        const int id = 2;

        var pokemons = await _pokemonService.Buscar(nome, id);

        using (new AssertionScope())
        {
            pokemons.Should().BeEmpty();
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
        }
    }

    [Fact]
    public async Task Buscar_PokemonsExistente_Sucesso()
    {
        SetupMocks(possuiPokemons: true);

        const string nome = "Pokemon";
        const int id = 1;

        var pokemons = await _pokemonService.Buscar(nome, id);

        using (new AssertionScope())
        {
            pokemons.Should().NotBeEmpty();
            pokemons.Should().BeOfType<List<PokemonDto>>();
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
        }
    }

    #endregion

    #region Atualizar

    [Fact]
    public async Task Atualizar_IdsNaoConferem_Handle()
    {
        SetupMocks();

        const int id = 2;
        var dto = new AtualizarPokemonDto
        {
            Id = 1
        };

        var pokemon = await _pokemonService.Atualizar(id, dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Once);
            NotificatorMock.Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.Atualizar(It.IsAny<Pokemon>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Never);
        }
    }

    [Fact]
    public async Task Atualizar_PokemonInexistente_HandleNotFoundResourse()
    {
        SetupMocks();

        const int id = 2;
        var dto = new AtualizarPokemonDto
        {
            Id = 2
        };

        var pokemon = await _pokemonService.Atualizar(id, dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
            NotificatorMock.Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            NotificatorMock.Verify(c => c.HandleNotFoundResourse(), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.Atualizar(It.IsAny<Pokemon>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Never);
        }
    }

    [Fact]
    public async Task Atualizar_PokemonInvalido_HandleErros()
    {
        SetupMocks(pokemonExistente: true);

        const int id = 1;
        var dto = new AtualizarPokemonDto
        {
            Id = 1
        };

        var pokemon = await _pokemonService.Atualizar(id, dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Exactly(2));
            NotificatorMock.Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.Atualizar(It.IsAny<Pokemon>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Never);
        }
    }


    [Fact]
    public async Task Atualizar_ErroAoComitar_Handle()
    {
        SetupMocks();

        const int id = 1;
        var dto = new AtualizarPokemonDto
        {
            Id = 1,
            Nome = "PokemonTeste",
            Descricao = "Descricao",
            PokemonTipoId = 1
        };

        var pokemon = await _pokemonService.Atualizar(id, dto);

        using (new AssertionScope())
        {
            pokemon.Should().BeNull();

            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Once);
            NotificatorMock.Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.Atualizar(It.IsAny<Pokemon>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Once);
        }
    }

    [Fact]
    public async Task Atualizar_Pokemon_Sucesso()
    {
        SetupMocks(commit: true);

        const int id = 1;
        var dto = new AtualizarPokemonDto
        {
            Id = 1,
            Nome = "PokemonTeste",
            Descricao = "Descricao",
            PokemonTipoId = 1
        };

        var pokemon = await _pokemonService.Atualizar(id, dto);

        using (new AssertionScope())
        {
            pokemon.Should().NotBeNull();

            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
            NotificatorMock.Verify(c => c.Handle(It.IsAny<List<ValidationFailure>>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.Atualizar(It.IsAny<Pokemon>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Once);
        }
    }

    #endregion

    #region Remover

    [Fact]
    public async Task Remover_PokemonInexistente_HandleNotFoundResourse()
    {
        SetupMocks();

        const int id = 2;

        await _pokemonService.Remover(id);

        using (new AssertionScope())
        {
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
            NotificatorMock.Verify(c => c.HandleNotFoundResourse(), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.Remover(It.IsAny<Pokemon>()), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Never);
        }
    }

    [Fact]
    public async Task Remover_ErroAoComitar_Handle()
    {
        SetupMocks(commit: false);

        const int id = 1;

        await _pokemonService.Remover(id);

        using (new AssertionScope())
        {
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Once);
            NotificatorMock.Verify(c => c.HandleNotFoundResourse(), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.Remover(It.IsAny<Pokemon>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Once);
        }
    }

    [Fact]
    public async Task Remover_Sucesso()
    {
        SetupMocks(commit: true);

        const int id = 1;

        await _pokemonService.Remover(id);

        using (new AssertionScope())
        {
            NotificatorMock.Verify(c => c.Handle(It.IsAny<string>()), Times.Never);
            NotificatorMock.Verify(c => c.HandleNotFoundResourse(), Times.Never);
            _pokemonRepositoryMock.Verify(c => c.Remover(It.IsAny<Pokemon>()), Times.Once);
            _pokemonRepositoryMock.Verify(c => c.UnitOfWork.Commit(), Times.Once);
        }
    }

    #endregion

    private void SetupMocks(bool pokemonExistente = false, bool commit = false, bool possuiPokemons = true)
    {
        var pokemon = new Pokemon
        {
            Nome = "PokemonTeste",
            Descricao = "DescricaoTeste",
            PokemonTipoId = 1,
            PokemonTipo = new PokemonTipo
            {
                Id = 1,
                Nome = "Tipo"
            }
        };

        #region PokemonRepository

        _pokemonRepositoryMock
            .Setup(c => c.FirstOrDefault(It.IsAny<Expression<Func<Pokemon, bool>>>()))
            .ReturnsAsync(pokemonExistente ? pokemon : null);

        _pokemonRepositoryMock
            .Setup(c => c.UnitOfWork.Commit())
            .ReturnsAsync(commit);

        _pokemonRepositoryMock
            .Setup(c => c.ObterPorId(It.Is<int>(id => id == 1)))
            .ReturnsAsync(pokemon);

        _pokemonRepositoryMock
            .Setup(c => c.ObterPorId(It.Is<int>(id => id != 1)))
            .ReturnsAsync(null as Pokemon);

        _pokemonRepositoryMock
            .Setup(c => c.ObterTodos())!
            .ReturnsAsync(possuiPokemons ? new List<Pokemon> { pokemon } : null);

        _pokemonRepositoryMock
            .Setup(c => c.Buscar(It.Is<string>(nome => nome == "Pokemon"), It.Is<int>(nome => nome == 1)))!
            .ReturnsAsync(possuiPokemons ? new List<Pokemon> { pokemon } : null);

        #endregion
    }
}