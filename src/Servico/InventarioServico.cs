using System;
using System.Collections.Generic;
using System.Linq;
using controle_de_estoque_ub.src.Modelo;

namespace controle_de_estoque_ub.src.Servico
{
    public class InventarioServico
    {
        private List<Produto> produtos = new List<Produto>();
        private List<Movimento> movimentos = new List<Movimento>();
        private readonly CsvArmazenamento armazenamento;

        public InventarioServico()
        {
            armazenamento = new CsvArmazenamento();
            CarregarDados();

            // Se não houver produtos carregados, adiciona exemplos
            if (produtos.Count == 0)
            {
                produtos.Add(new Produto(1, "Mouse", "Periféricos", 5, 10));
                produtos.Add(new Produto(2, "Teclado", "Periféricos", 3, 5));
                produtos.Add(new Produto(3, "Monitor", "Monitores", 2, 2));
                produtos.Add(new Produto(4, "Webcam", "Periféricos", 5, 1)); // Abaixo do mínimo
            }
        }

        /// <summary>
        /// Carrega dados dos arquivos CSV
        /// </summary>
        private void CarregarDados()
        {
            try
            {
                var (produtosCarregados, movimentosCarregados) = armazenamento.CarregarDados();
                produtos = produtosCarregados;
                movimentos = movimentosCarregados;

                Console.ForegroundColor = ConsoleColor.Green;
                Program.EscreverCentralizado($"✓ {produtos.Count} produtos e {movimentos.Count} movimentos carregados.");
                Console.ResetColor();
                System.Threading.Thread.Sleep(800);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Program.EscreverCentralizado($"⚠ Aviso ao carregar dados: {ex.Message}");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Cadastra novo produto no sistema
        /// </summary>
        public void CadastrarProduto()
        {
            Console.Clear();
            MostrarCabecalho("CADASTRAR NOVO PRODUTO");

            Console.Write("ID do novo produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                MostrarErro("ID inválido! Deve ser um número positivo.");
                return;
            }

            if (produtos.Exists(p => p.Id == id))
            {
                MostrarErro("Já existe um produto com esse ID!");
                return;
            }

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nome))
            {
                MostrarErro("Nome é obrigatório!");
                return;
            }

            Console.Write("Categoria: ");
            string categoria = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Estoque mínimo: ");
            if (!int.TryParse(Console.ReadLine(), out int estoqueMinimo) || estoqueMinimo < 0)
            {
                MostrarErro("Estoque mínimo inválido! Deve ser >= 0.");
                return;
            }

            Console.Write("Saldo inicial: ");
            if (!int.TryParse(Console.ReadLine(), out int saldo) || saldo < 0)
            {
                MostrarErro("Saldo inválido! Deve ser >= 0.");
                return;
            }

            produtos.Add(new Produto(id, nome, categoria, estoqueMinimo, saldo));
            MostrarSucesso("Produto cadastrado com sucesso!");
        }

        /// <summary>
        /// Edita um produto existente
        /// </summary>
        public void EditarProduto()
        {
            Console.Clear();
            MostrarCabecalho("EDITAR PRODUTO");

            Console.Write("ID do produto a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarErro("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == id);
            if (produto == null)
            {
                MostrarErro("Produto não encontrado!");
                return;
            }

            Console.WriteLine($"\nProduto atual: {produto.Nome} | Categoria: {produto.Categoria}");
            Console.WriteLine($"Estoque Mín: {produto.EstoqueMinimo} | Saldo: {produto.Saldo}\n");

            Console.Write($"Novo nome (Enter para manter '{produto.Nome}'): ");
            string novoNome = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(novoNome))
                produto.Nome = novoNome;

            Console.Write($"Nova categoria (Enter para manter '{produto.Categoria}'): ");
            string novaCategoria = Console.ReadLine()?.Trim();
            if (novaCategoria != null)
                produto.Categoria = novaCategoria;

            Console.Write($"Novo estoque mínimo (Enter para manter {produto.EstoqueMinimo}): ");
            string inputMin = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(inputMin))
            {
                if (int.TryParse(inputMin, out int novoMin) && novoMin >= 0)
                    produto.EstoqueMinimo = novoMin;
                else
                {
                    MostrarErro("Estoque mínimo inválido! Deve ser >= 0.");
                    return;
                }
            }

            Console.Write($"Novo saldo (Enter para manter {produto.Saldo}): ");
            string inputSaldo = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(inputSaldo))
            {
                if (int.TryParse(inputSaldo, out int novoSaldo) && novoSaldo >= 0)
                    produto.Saldo = novoSaldo;
                else
                {
                    MostrarErro("Saldo inválido! Deve ser >= 0.");
                    return;
                }
            }

            MostrarSucesso("Produto atualizado com sucesso!");
        }

        /// <summary>
        /// Exclui um produto (apenas se saldo = 0)
        /// </summary>
        public void ExcluirProduto()
        {
            Console.Clear();
            MostrarCabecalho("EXCLUIR PRODUTO");

            Console.Write("ID do produto a excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarErro("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == id);
            if (produto == null)
            {
                MostrarErro("Produto não encontrado!");
                return;
            }

            // Validação: não permitir remoção se houver saldo
            if (produto.Saldo > 0)
            {
                MostrarErro($"Não é possível excluir! O produto possui saldo de {produto.Saldo} unidades.");
                Console.WriteLine("Dica: Faça saídas de estoque até zerar o saldo antes de excluir.");
                return;
            }

            Console.WriteLine($"\nProduto: {produto.Nome}");
            Console.Write("Confirma exclusão? (S/N): ");
            string confirmacao = Console.ReadLine()?.Trim().ToUpper();

            if (confirmacao == "S")
            {
                produtos.Remove(produto);
                MostrarSucesso("Produto excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Exclusão cancelada.");
            }
        }

        /// <summary>
        /// Lista todos os produtos cadastrados
        /// </summary>
        public void ListarProdutos()
        {
            Console.Clear();
            MostrarCabecalho("LISTA DE PRODUTOS");

            if (produtos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Program.EscreverCentralizado("Nenhum produto cadastrado.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nome",-25} {"Categoria",-20} {"Mín",-6} {"Saldo",-8} {"Status",-15}");
            Console.WriteLine(new string('-', 85));

            foreach (var p in produtos.OrderBy(x => x.Id))
            {
                string status = p.Saldo < p.EstoqueMinimo ? "⚠ ABAIXO MÍN" : "OK";
                ConsoleColor cor = p.Saldo < p.EstoqueMinimo ? ConsoleColor.Red : ConsoleColor.White;

                Console.ForegroundColor = cor;
                Console.WriteLine($"{p.Id,-5} {p.Nome,-25} {p.Categoria,-20} {p.EstoqueMinimo,-6} {p.Saldo,-8} {status,-15}");
                Console.ResetColor();
            }

            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Total de produtos: {produtos.Count}");
        }

        /// <summary>
        /// Registra entrada de estoque
        /// </summary>
        public void EntradaEstoque()
        {
            Console.Clear();
            MostrarCabecalho("ENTRADA DE ESTOQUE");

            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                MostrarErro("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == produtoId);
            if (produto == null)
            {
                MostrarErro("Produto não encontrado!");
                return;
            }

            Console.WriteLine($"Produto: {produto.Nome} | Saldo atual: {produto.Saldo}");

            Console.Write("Quantidade a adicionar: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
            {
                MostrarErro("Quantidade inválida! Deve ser maior que 0.");
                return;
            }

            Console.Write("Observação: ");
            string obs = Console.ReadLine()?.Trim() ?? "";

            // Atualiza saldo
            produto.Saldo += qtd;

            // Registra movimento
            var mov = new Movimento(
                movimentos.Count + 1,
                produto.Id,
                "ENTRADA",
                qtd,
                DateTime.Now,
                obs
            );

            movimentos.Add(mov);

            Console.WriteLine($"\n✓ Novo saldo: {produto.Saldo}");
            MostrarSucesso("Entrada registrada com sucesso!");
        }

        /// <summary>
        /// Registra saída de estoque (com validação de saldo)
        /// </summary>
        public void SaidaEstoque()
        {
            Console.Clear();
            MostrarCabecalho("SAÍDA DE ESTOQUE");

            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                MostrarErro("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == produtoId);
            if (produto == null)
            {
                MostrarErro("Produto não encontrado!");
                return;
            }

            Console.WriteLine($"Produto: {produto.Nome} | Saldo atual: {produto.Saldo}");

            Console.Write("Quantidade a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
            {
                MostrarErro("Quantidade inválida! Deve ser maior que 0.");
                return;
            }

            // Validação: saldo insuficiente
            if (produto.Saldo < qtd)
            {
                MostrarErro($"Saldo insuficiente! Disponível: {produto.Saldo}, Solicitado: {qtd}");
                return;
            }

            Console.Write("Observação: ");
            string obs = Console.ReadLine()?.Trim() ?? "";

            // Atualiza saldo
            produto.Saldo -= qtd;

            // Registra movimento
            var mov = new Movimento(
                movimentos.Count + 1,
                produto.Id,
                "SAIDA",
                qtd,
                DateTime.Now,
                obs
            );

            movimentos.Add(mov);

            Console.WriteLine($"\n✓ Novo saldo: {produto.Saldo}");

            // Alerta se ficou abaixo do mínimo
            if (produto.Saldo < produto.EstoqueMinimo)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠ ATENÇÃO: Estoque abaixo do mínimo! (Mínimo: {produto.EstoqueMinimo})");
                Console.ResetColor();
            }

            MostrarSucesso("Saída registrada com sucesso!");
        }

        /// <summary>
        /// Relatório de produtos abaixo do estoque mínimo
        /// </summary>
        public void RelatorioAbaixoMinimo()
        {
            Console.Clear();
            MostrarCabecalho("RELATÓRIO: ESTOQUE ABAIXO DO MÍNIMO");

            var produtosAbaixo = produtos.Where(p => p.Saldo < p.EstoqueMinimo).OrderBy(p => p.Saldo).ToList();

            if (produtosAbaixo.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Program.EscreverCentralizado("✓ Todos os produtos estão com estoque adequado!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n⚠ {produtosAbaixo.Count} produto(s) abaixo do estoque mínimo:\n");
            Console.ResetColor();

            Console.WriteLine($"{"ID",-5} {"Nome",-25} {"Categoria",-20} {"Mín",-6} {"Saldo",-8} {"Déficit",-10}");
            Console.WriteLine(new string('-', 80));

            foreach (var p in produtosAbaixo)
            {
                int deficit = p.EstoqueMinimo - p.Saldo;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{p.Id,-5} {p.Nome,-25} {p.Categoria,-20} {p.EstoqueMinimo,-6} {p.Saldo,-8} {deficit,-10}");
                Console.ResetColor();
            }

            Console.WriteLine(new string('-', 80));
        }

        /// <summary>
        /// Relatório de movimentações por produto
        /// </summary>
        public void ExtratoPorProduto()
        {
            Console.Clear();
            MostrarCabecalho("EXTRATO DE MOVIMENTOS POR PRODUTO");

            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                MostrarErro("ID inválido.");
                return;
            }

            var produto = produtos.Find(p => p.Id == produtoId);
            if (produto == null)
            {
                MostrarErro("Produto não encontrado!");
                return;
            }

            var movsProduto = movimentos.Where(m => m.ProdutoId == produtoId).OrderBy(m => m.Data).ToList();

            Console.Clear();
            MostrarCabecalho($"EXTRATO: {produto.Nome}");
            Console.WriteLine($"Categoria: {produto.Categoria} | Estoque Mín: {produto.EstoqueMinimo} | Saldo Atual: {produto.Saldo}\n");

            if (movsProduto.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Program.EscreverCentralizado("Nenhuma movimentação registrada para este produto.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"{"Data/Hora",-20} {"Tipo",-10} {"Qtd",-8} {"Observação",-40}");
            Console.WriteLine(new string('-', 85));

            int totalEntradas = 0;
            int totalSaidas = 0;

            foreach (var m in movsProduto)
            {
                ConsoleColor cor = m.Tipo == "ENTRADA" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.ForegroundColor = cor;

                Console.WriteLine($"{m.Data:dd/MM/yyyy HH:mm:ss,-20} {m.Tipo,-10} {m.Quantidade,-8} {m.Observacao,-40}");
                Console.ResetColor();

                if (m.Tipo == "ENTRADA")
                    totalEntradas += m.Quantidade;
                else
                    totalSaidas += m.Quantidade;
            }

            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Total de movimentos: {movsProduto.Count}");
            Console.WriteLine($"Total Entradas: {totalEntradas} | Total Saídas: {totalSaidas} | Saldo: {totalEntradas - totalSaidas}");
        }

        /// <summary>
        /// Salva todos os dados em CSV (operação completa)
        /// </summary>
        public void SalvarDados()
        {
            Console.Clear();
            MostrarCabecalho("SALVAR DADOS");

            try
            {
                armazenamento.SalvarDados(produtos, movimentos);

                Console.ForegroundColor = ConsoleColor.Green;
                Program.EscreverCentralizado("✓ Dados salvos com sucesso!");
                Program.EscreverCentralizado($"{produtos.Count} produtos e {movimentos.Count} movimentos gravados.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                MostrarErro($"Erro ao salvar: {ex.Message}");
            }
        }

        // Métodos auxiliares para formatação
        private void MostrarCabecalho(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string borda = new string('═', titulo.Length + 4);
            Program.EscreverCentralizado($"╔{borda}╗");
            Program.EscreverCentralizado($"║  {titulo}  ║");
            Program.EscreverCentralizado($"╚{borda}╝\n");
            Console.ResetColor();
        }

        private void MostrarSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {mensagem}");
            Console.ResetColor();
        }

        private void MostrarErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ {mensagem}");
            Console.ResetColor();
        }
    }
}