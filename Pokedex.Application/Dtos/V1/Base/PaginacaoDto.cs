using Pokedex.Domain.Contracts.Paginacao;

namespace Pokedex.Application.Dtos.V1.Base;

public class PaginacaoDto : IPaginacao
{
    public int Total { get; set; }
    public int TotalNaPagina { get; set; }
    public int Pagina { get; set; }
    public int TamanhoPagina { get; set; }
    public int TotalDePaginas { get; set; }

    public int LastPage => TotalDePaginas;

    public bool HasPages => TotalDePaginas > 0;

    public bool OnFirstPage => Pagina == 1;

    public bool OnLastPage => Pagina == TotalDePaginas;

    public bool HasMorePages => TotalDePaginas > Pagina;
}
