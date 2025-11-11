using System;

namespace controle_de_estoque_ub.src.Modelo
{
    /// <summary>
    /// Representa uma movimentação de estoque (entrada ou saída)
    /// </summary>
    public record struct Movimento
    {
        public int Id { get; init; }
        public int ProdutoId { get; init; }
        public string Tipo { get; init; }  // "ENTRADA" ou "SAIDA"
        public int Quantidade { get; init; }
        public DateTime Data { get; init; }
        public string Observacao { get; init; }

        public Movimento(int id, int produtoId, string tipo, int quantidade, DateTime data, string observacao)
        {
            Id = id;
            ProdutoId = produtoId;
            Tipo = tipo;
            Quantidade = quantidade;
            Data = data;
            Observacao = observacao;
        }
    }
}