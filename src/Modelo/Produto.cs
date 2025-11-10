namespace controle_de_estoque_ub.src.Modelo
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int EstoqueMinimo { get; set; }
        public int Saldo { get; set; }

        public Produto(int id, string nome, int saldo)
        {
            Id = id;
            Nome = nome;
            Categoria = "";
            EstoqueMinimo = 0;
            Saldo = saldo;
        }

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
