using System;

namespace controle_de_estoque_ub.src.Modelo
{
    public record struct Movimento(
        int Id,
        int ProdutoId,
        string Tipo,
        int Quantidade,
        DateTime Data,
        string Observacao
    );
}
