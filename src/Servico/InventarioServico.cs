using controle_de_estoque_ub.src.Modelo;

namespace controle_de_estoque_ub.src.Servico
{
    public class InventarioServico
    {
        private List<Produto> produtos = new List<Produto>();     // lista que armazena os produtos do sistema
        private List<Movimento> movimentos = new List<Movimento>(); // lista que guarda todas as movimentações de entrada e saída
        private const string CaminhoMovimentos = "movimentos.csv"; // caminho do CSV onde os movimentos serão salvos

        public InventarioServico()
        {
            produtos.Add(new Produto(1, "Mouse", 10)); //produtos de exemplo 
            produtos.Add(new Produto(2, "Teclado", 5));
            produtos.Add(new Produto(3, "Monitor", 2));
        }

        public void CadastrarProduto()
        {
            Console.Write("ID do novo produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) //verifica se o id é valido
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            if (produtos.Exists(p => p.Id == id)) //procura o id do produto
            {
                Console.WriteLine("Já existe um produto com esse ID!"); //se o id for o mesmo, escreve
                return;
            }

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine() ?? ""; //pede o nome

            Console.Write("Saldo inicial: ");
            if (!int.TryParse(Console.ReadLine(), out int saldo)) //verifica se da pra converter o valor
            {
                Console.WriteLine("Saldo inválido.");
                return;
            }

            produtos.Add(new Produto(id, nome, saldo));
            Console.WriteLine("Produto cadastrado com sucesso!");
        }

        public void EditarProduto()
        {
            Console.Write("ID do produto a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) //verifica se o id é valido
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == id); //procura o id
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            Console.Write($"Novo nome (atual: {produto.Nome}): "); //mostra o nome do produto
            string novoNome = Console.ReadLine() ?? produto.Nome;

            Console.Write($"Novo saldo (atual: {produto.Saldo}): ");
            if (!int.TryParse(Console.ReadLine(), out int novoSaldo))
            {
                Console.WriteLine("Saldo inválido.");
                return;
            }

            produto.Nome = novoNome;
            produto.Saldo = novoSaldo;

            Console.WriteLine("Produto atualizado com sucesso!");
        }

        public void ExcluirProduto()
        {
            Console.Write("ID do produto a excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) //verifica o id
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == id); //procura o id
            if (produto == null) //verifica se o id existe
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            produtos.Remove(produto);
            Console.WriteLine("Produto excluído com sucesso!");
        }

        public void ListarProdutos()
        {
            Console.WriteLine("\n=== LISTA DE PRODUTOS ===");
            foreach (var p in produtos) //lista o dicionario por id nome e saldo
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Saldo: {p.Saldo}");
            }
            Console.WriteLine("==========================\n");
        }

        public void EntradaEstoque()
        {
            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                Console.WriteLine("ID inválido."); //se nao encontrar o id, escreve
                return;
            }

            var produto = produtos.Find(p => p.Id == produtoId); //procura  o id
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            Console.Write("Quantidade a adicionar: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0) //pede a quantidade
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            Console.Write("Observação: ");
            string obs = Console.ReadLine() ?? "";

            produto.Saldo += qtd; //atualiza o saldo automaticamente

            var mov = new Movimento
            {
                Id = movimentos.Count + 1,
                ProdutoId = produto.Id,
                Tipo = "Entrada",
                Quantidade = qtd,
                Data = DateTime.Now,
                Observacao = obs
            };

            movimentos.Add(mov);
            SalvarDados(mov);
            Console.WriteLine("entrada registrada com sucesso");
        }

        public void SaidaEstoque()
        {
            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == produtoId); //procura o id para saida de estoque
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            Console.Write("Quantidade a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0) //a quantidade precisa ser maior que 0
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            if (produto.Saldo < qtd)
            {
                Console.WriteLine("Saldo insuficiente para a saída!");
                return;
            }

            Console.Write("Observação: ");
            string obs = Console.ReadLine() ?? "";

            produto.Saldo -= qtd; //atualiza o saldo automaticamente

            var mov = new Movimento
            {
                Id = movimentos.Count + 1,
                ProdutoId = produto.Id,
                Tipo = "Saída",
                Quantidade = qtd,
                Data = DateTime.Now,
                Observacao = obs
            };

            movimentos.Add(mov);
            SalvarDados(mov);
            Console.WriteLine("Saída registrada com sucesso!");
        }


        public void RelatorioAbaixoMinimo()
        {
            Console.WriteLine("Simulação: Relatório de produtos abaixo do mínimo...");
        }

        public void ExtratoPorProduto()
        {
            Console.WriteLine("Simulação: Extrato por produto...");
        }


        public void SalvarDados(Movimento mov)
        {
            bool arquivoExiste = File.Exists(CaminhoMovimentos);

            using (var sw = new StreamWriter(CaminhoMovimentos, append: true))
            {
                if (!arquivoExiste)
                    sw.WriteLine("Id,ProdutoId,Tipo,Quantidade,Data,Observacao");

                sw.WriteLine($"{mov.Id},{mov.ProdutoId},{mov.Tipo},{mov.Quantidade},{mov.Data:o},{EscapeCsv(mov.Observacao)}");
            }
        }

        // usado quando for chamada sem parâmetro
        public void SalvarDados()
        {
            Console.WriteLine("Simulação: Dados salvos com sucesso!");
        }

        private string EscapeCsv(string campo) //declara a função que recebe uma string
        {
            if (string.IsNullOrEmpty(campo)) return ""; //se o campo for vazio, retorna a string vazia
            if (campo.Contains(",") || campo.Contains("\"") || campo.Contains("\n") || campo.Contains("\r")) //verifica se o texto tem aspas duplas ou quebras de linha (quebram o csv)
            {
                return $"\"{campo.Replace("\"", "\"\"")}\""; //se tiver aspas duplas, converte no formato certo do csv
            }
            return campo; //se o campo nao tiver caracteres especiais, ele retorna normal
        }
    }
}