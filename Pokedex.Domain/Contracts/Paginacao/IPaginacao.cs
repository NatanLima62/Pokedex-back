namespace Pokedex.Domain.Contracts.Paginacao;

public interface IPaginacao
{
    public int Total { get; set; }
    public int TotalNaPagina { get; set; }
    public int Pagina { get; set; }
    public int TamanhoPagina { get; set; }
    public int TotalDePaginas { get; set; }
}