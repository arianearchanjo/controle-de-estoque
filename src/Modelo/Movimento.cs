using System;

namespace controle_de_estoque_ub.src.Modelo
{
    /// Representa uma movimentação de estoque (entrada ou saída)
    /// Utiliza record struct para imutabilidade e rastreamento de histórico
  
    public record struct Movimento
    {
        /// Identificador único do movimento
        public int Id { get; init; }

        /// ID do produto relacionado a este movimento
        public int ProdutoId { get; init; }

        /// Tipo de movimento: "ENTRADA" ou "SAIDA"
        public string Tipo { get; init; }

        /// Quantidade de itens movimentados
        /// Deve ser maior que 0
        public int Quantidade { get; init; }

        /// Data e hora em que o movimento foi registrado
        public DateTime Data { get; init; }

        /// Observação opcional sobre o movimento
        /// Útil para rastreamento e auditoria
        public string Observacao { get; init; }

        /// Construtor para criar um novo movimento de estoque
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