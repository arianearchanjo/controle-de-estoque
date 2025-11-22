using System;

namespace controle_de_estoque_ub.src.Modelo
{
    /// <summary>
    /// Representa uma movimentação de estoque (entrada ou saída)
    /// Utiliza record struct para imutabilidade e rastreamento de histórico
    /// </summary>
    public record struct Movimento
    {
        /// <summary>
        /// Identificador único do movimento
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// ID do produto relacionado a este movimento
        /// </summary>
        public int ProdutoId { get; init; }

        /// <summary>
        /// Tipo de movimento: "ENTRADA" ou "SAIDA"
        /// </summary>
        public string Tipo { get; init; }

        /// <summary>
        /// Quantidade de itens movimentados
        /// Deve ser maior que 0
        /// </summary>
        public int Quantidade { get; init; }

        /// <summary>
        /// Data e hora em que o movimento foi registrado
        /// </summary>
        public DateTime Data { get; init; }

        /// <summary>
        /// Observação opcional sobre o movimento
        /// Útil para rastreamento e auditoria
        /// </summary>
        public string Observacao { get; init; }

        /// <summary>
        /// Construtor para criar um novo movimento de estoque
        /// </summary>
        /// <param name="id">ID único do movimento</param>
        /// <param name="produtoId">ID do produto movimentado</param>
        /// <param name="tipo">Tipo: "ENTRADA" ou "SAIDA"</param>
        /// <param name="quantidade">Quantidade movimentada</param>
        /// <param name="data">Data/hora do movimento</param>
        /// <param name="observacao">Observação sobre o movimento</param>
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