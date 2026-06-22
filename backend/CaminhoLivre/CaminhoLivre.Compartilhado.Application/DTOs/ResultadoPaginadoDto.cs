namespace CaminhoLivre.Compartilhado.DTOs;

public record ResultadoPaginadoDto<T>(IEnumerable<T> Itens, int TotalItens, int PaginaAtual, int QuantidadePorPagina);
