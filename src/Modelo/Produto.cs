namespace controle_de_estoque_ub.src.Modelo
{
    /// Representa um produto no sistema de controle de estoque
    /// Utiliza record struct para imutabilidade e performance
    public record struct Produto
    {
        /// Identificador único do produto
        public int Id { get; set; }
        =
        /// Nome do produto (campo obrigatório)
        public string Nome { get; set; }

        /// Categoria do produto para organização
        public string Categoria { get; set; }

        /// Quantidade mínima que deve ser mantida em estoque
        /// Deve ser maior ou igual a 0
        public int EstoqueMinimo { get; set; }

        /// Quantidade atual disponível em estoque
        /// Deve ser maior ou igual a 0
        public int Saldo { get; set; }

        /// Construtor simplificado para criação rápida de produto
        public Produto(int id, string nome, int saldo)
        {
            Id = id;
            Nome = nome;
            Categoria = "";
            EstoqueMinimo = 0;
            Saldo = saldo;
        }

        /// Construtor completo para criação de produto com todos os campos
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