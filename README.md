## Desafio - Pokedéx

#### Objetivo:
Criar uma API Rest usando .NET 6 
- O sistema deverá poder realizar todas as operações de CRUD de pokemons.

#### Requisitos Gerais
1. O backend será uma API Rest.
2. Utilizar .NET 6. [SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
3. Utilizar Migrations com EF Core para criar as tabelas no banco. [Doc EF Core](https://docs.microsoft.com/pt-br/ef/core/)
4. Como usamos MySQL, para isso vamos usar o [Pomelo](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql) junto com o EF Core.
5. Para gerar e executar as migrations:
   1. Usando o rider:
      1. Criar migration: Vá em `tools > Entity Framework Core > Add Migration`. 
      2. Atualizar banco: Vá em `tools > Entity Framework Core > Update Database`
   2. Package Manager Console (Visual Studio) - Setar o projeto corrente para o projeto de **Infra.Data**
      1. Criar migration: ```Add-Migration <migration_nome> -Context "ApplicationPokedexDbContext"```
      2. Atualizar banco: ```Update-Database -Context "ApplicationPokedexDbContext"```
   3. dotnet ef (Executar comandos da raiz do projeto)
      1. Criar migration: ```dotnet ef migrations add <migration_nome> -c ApplicationPokedexDbContext -s "src\Pokedex.API" -p "src\Pokedex.Infra"```
      2. Atualizar banco: ```dotnet ef database update -c ApplicationPokedexDbContext -s "src\Pokedex.API" -p "src\IFCE.Intranet.Infra"```
6. Nos .csproj onde tiver `<Nullable>enable</Nullable>` alterar para `<Nullable>disable</Nullable>` para manter compatibilidade com o exemplo.
---

#### Dependencias
 - .NET 6
 - Entity Framerwork Core 6
 - MySQL
 - AutoMapper
 - FluentValidation

#### Descrição de Entidades

 - Pokemon
   - Id
   - Nome
   - Descricao
   - PokemonTipoId
 - PokemonTipo
 - Id
 - Nome
   
---

#### Registro do pokemón
 - [ ] Pedir `Nome`, `Descricao`, `PokemonTipoId`
 - [ ] Deve ser verificado se o `Nome` já está em uso.
