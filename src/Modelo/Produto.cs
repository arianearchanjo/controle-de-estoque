namespace controle_de_estoque_ub.src.Modelo
{
    /// <summary>
    /// Representa um produto no sistema de controle de estoque
    /// Utiliza record struct para imutabilidade e performance
    /// </summary>
    public record struct Produto
    {
        /// <summary>
        /// Identificador único do produto
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto (campo obrigatório)
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Categoria do produto para organização
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Quantidade mínima que deve ser mantida em estoque
        /// Deve ser maior ou igual a 0
        /// </summary>
        public int EstoqueMinimo { get; set; }

        /// <summary>
        /// Quantidade atual disponível em estoque
        /// Deve ser maior ou igual a 0
        /// </summary>
        public int Saldo { get; set; }

        /// <summary>
        /// Construtor simplificado para criação rápida de produto
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="nome">Nome do produto</param>
        /// <param name="saldo">Saldo inicial</param>
        public Produto(int id, string nome, int saldo)
        {
            Id = id;
            Nome = nome;
            Categoria = "";
            EstoqueMinimo = 0;
            Saldo = saldo;
        }

        /// <summary>
        /// Construtor completo para criação de produto com todos os campos
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="nome">Nome do produto</param>
        /// <param name="categoria">Categoria do produto</param>
        /// <param name="estoqueMinimo">Estoque mínimo aceitável</param>
        /// <param name="saldo">Saldo atual em estoque</param>
        public Produto(int id, string nome, string categoria, int estoqueMinimo, int saldo)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            EstoqueMinimo = estoqueMinimo;
            Saldo = saldo;
        }
    }
}