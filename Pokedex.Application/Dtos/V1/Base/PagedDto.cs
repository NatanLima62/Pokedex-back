using Pokedex.Domain.Contracts.Paginacao;

namespace Pokedex.Application.Dtos.V1.Base;

public class PagedDto<T> : IResultadoPaginado<T>
{
    public IList<T> Itens { get; set; }
    public IPaginacao Paginacao { get; set; }

    public PagedDto()
    {
        Itens = new List<T>();
        Paginacao = new PaginacaoDto();
    }
} 
