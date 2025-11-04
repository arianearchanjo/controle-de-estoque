using System;
using System.Threading;
using controle_de_estoque_ub.src.Servico;

namespace controle_de_estoque_ub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Controle de Estoque - Projeto 2º Bimestre";
            Console.Clear();
            Console.CursorVisible = false;

            MostrarCreditos();
            new MenuUI().MostrarPrincipal(new InventarioServico());
        }

        static void MostrarCreditos()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
            EscreverCentralizado("╔═══════════════════════════════════════════════╗");
            EscreverCentralizado("║         CONTROLE DE ESTOQUE - 2º BIMESTRE     ║");
            EscreverCentralizado("╚═══════════════════════════════════════════════╝\n");
            Console.ResetColor();

            string[] alunos =
            {
                "Ariane da Silva Archanjo - 2025106857",
                "Caio Melo Canhetti - 2025104636",
                "Lucas Vinicius Barros Dias - 2025105450",
                "Matheus Sizanoski Figueiredo - 2025105007",
                "Pedro Henrique Kafka Zaratino - 2025105057",
                "Rafael Martins Schreurs Sales - 2025105454"
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            EscreverCentralizado("DESENVOLVIDO POR:\n");
            Console.ResetColor();

            foreach (var nome in alunos)
            {
                EscreverCentralizado(nome);
                Thread.Sleep(300);
            }

            Thread.Sleep(400);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            EscreverCentralizado("Professor: Marlos Alex de Oliveira Marques\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            EscreverCentralizado("Pressione qualquer tecla para iniciar...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void EscreverCentralizado(string texto)
        {
            int largura = Console.WindowWidth;
            int posicao = (largura - texto.Length) / 2;
            if (posicao < 0) posicao = 0;
            Console.SetCursorPosition(posicao, Console.CursorTop);
            Console.WriteLine(texto);
        }
    }

    class MenuUI
    {
        public void MostrarPrincipal(InventarioServico servico)
        {
            int opcao;
            do
            {
                Console.Clear();
                MostrarCabecalho("SISTEMA DE CONTROLE DE ESTOQUE");

                string[] opcoes =
                {
                    "1 - Listar produtos",
                    "2 - Cadastrar produto",
                    "3 - Editar produto",
                    "4 - Excluir produto",
                    "5 - Dar ENTRADA em estoque",
                    "6 - Dar SAÍDA de estoque",
                    "7 - Relatório: Estoque abaixo do mínimo",
                    "8 - Relatório: Extrato por produto",
                    "9 - Salvar dados (CSV)",
                    "0 - Sair"
                };

                for (int i = 0; i < opcoes.Length; i++)
                {
                    Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.White : ConsoleColor.Gray;
                    Program.EscreverCentralizado(opcoes[i]);
                }
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Program.EscreverCentralizado("---------------------------------------------");
                Console.ResetColor();

                Program.EscreverCentralizado("\nEscolha uma opção: ");
                Console.CursorVisible = true;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 1, Console.CursorTop);
                string entrada = Console.ReadLine();
                Console.CursorVisible = false;
                Console.Clear();

                if (!int.TryParse(entrada, out opcao))
                {
                    MensagemTemporaria("Entrada inválida! Digite um número entre 0 e 9.", ConsoleColor.Red);
                    continue;
                }

                try
                {
                    switch (opcao)
                    {
                        case 1: servico.ListarProdutos(); break;
                        case 2: servico.CadastrarProduto(); break;
                        case 3: servico.EditarProduto(); break;
                        case 4: servico.ExcluirProduto(); break;
                        case 5: servico.EntradaEstoque(); break;
                        case 6: servico.SaidaEstoque(); break;
                        case 7: servico.RelatorioAbaixoMinimo(); break;
                        case 8: servico.ExtratoPorProduto(); break;
                        case 9: servico.SalvarDados(); break;
                        case 0: EncerrarSistema(); break;
                        default:
                            MensagemTemporaria("Opção inválida. Tente novamente.", ConsoleColor.Yellow);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MensagemTemporaria($"Erro: {ex.Message}", ConsoleColor.Red);
                }

                if (opcao != 0)
                {
                    Console.WriteLine();
                    Program.EscreverCentralizado("Pressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        void MostrarCabecalho(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string borda = new string('═', 35);
            Program.EscreverCentralizado($"╔{borda}╗");
            Program.EscreverCentralizado($"║  {titulo.ToUpper().PadRight(33)}║");
            Program.EscreverCentralizado($"╚{borda}╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Program.EscreverCentralizado($"Data: {DateTime.Now:dd/MM/yyyy} | Hora: {DateTime.Now:HH:mm:ss}\n");
            Console.ResetColor();
        }

        void EncerrarSistema()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Program.EscreverCentralizado("Encerrando o sistema...");
            Console.ResetColor();
            Thread.Sleep(500);

            int larguraBarra = 30;
            for (int i = 0; i <= larguraBarra; i++)
            {
                Console.Clear();
                string barra = new string('=', i).PadRight(larguraBarra);
                Program.EscreverCentralizado($"Saindo [{barra}] {i * 100 / larguraBarra}%");
                Thread.Sleep(70);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Program.EscreverCentralizado("Obrigado por utilizar o Controle de Estoque!");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        void MensagemTemporaria(string texto, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Program.EscreverCentralizado(texto);
            Console.ResetColor();
            Thread.Sleep(1000);
        }
    }
}
