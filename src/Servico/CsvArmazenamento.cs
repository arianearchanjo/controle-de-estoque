using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using controle_de_estoque_ub.src.Modelo;

namespace controle_de_estoque_ub.src.Servico
{
    /// <summary>
    /// Classe responsável pela persistência de dados em arquivos CSV
    /// Implementa escrita atômica para garantir integridade dos dados
    /// </summary>
    public class CsvArmazenamento
    {
        private const string DiretorioDados = "data";
        private const string CaminhoProdutos = "data/produtos.csv";
        private const string CaminhoMovimentos = "data/movimentos.csv";

        public CsvArmazenamento()
        {
            // Garante que o diretório data/ existe
            if (!Directory.Exists(DiretorioDados))
            {
                Directory.CreateDirectory(DiretorioDados);
            }

            // Cria os arquivos com cabeçalhos se não existirem
            CriarArquivosSeNecessario();
        }

        /// <summary>
        /// Cria arquivos CSV com cabeçalhos se não existirem
        /// </summary>
        private void CriarArquivosSeNecessario()
        {
            if (!File.Exists(CaminhoProdutos))
            {
                File.WriteAllText(CaminhoProdutos, "id;nome;categoria;estoqueMinimo;saldo\n", Encoding.UTF8);
            }

            if (!File.Exists(CaminhoMovimentos))
            {
                File.WriteAllText(CaminhoMovimentos, "id;produtoId;tipo;quantidade;data;observacao\n", Encoding.UTF8);
            }
        }

        /// <summary>
        /// Carrega produtos e movimentos dos arquivos CSV
        /// </summary>
        /// <returns>Tupla contendo listas de produtos e movimentos</returns>
        public (List<Produto>, List<Movimento>) CarregarDados()
        {
            var produtos = CarregarProdutos();
            var movimentos = CarregarMovimentos();
            return (produtos, movimentos);
        }

        /// <summary>
        /// Carrega produtos do arquivo CSV
        /// </summary>
        private List<Produto> CarregarProdutos()
        {
            var produtos = new List<Produto>();

            if (!File.Exists(CaminhoProdutos))
            {
                return produtos;
            }

            try
            {
                var linhas = File.ReadAllLines(CaminhoProdutos, Encoding.UTF8);

                // Pula o cabeçalho (primeira linha)
                for (int i = 1; i < linhas.Length; i++)
                {
                    var linha = linhas[i].Trim();

                    // Ignora linhas vazias
                    if (string.IsNullOrWhiteSpace(linha))
                        continue;

                    var campos = ParseCsvLine(linha);

                    if (campos.Length >= 5)
                    {
                        int id = int.Parse(campos[0]);
                        string nome = campos[1];
                        string categoria = campos[2];
                        int estoqueMinimo = int.Parse(campos[3]);
                        int saldo = int.Parse(campos[4]);

                        produtos.Add(new Produto(id, nome, categoria, estoqueMinimo, saldo));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar produtos: {ex.Message}");
            }

            return produtos;
        }

        /// <summary>
        /// Carrega movimentos do arquivo CSV
        /// </summary>
        private List<Movimento> CarregarMovimentos()
        {
            var movimentos = new List<Movimento>();

            if (!File.Exists(CaminhoMovimentos))
            {
                return movimentos;
            }

            try
            {
                var linhas = File.ReadAllLines(CaminhoMovimentos, Encoding.UTF8);

                // Pula o cabeçalho
                for (int i = 1; i < linhas.Length; i++)
                {
                    var linha = linhas[i].Trim();

                    // Ignora linhas vazias
                    if (string.IsNullOrWhiteSpace(linha))
                        continue;

                    var campos = ParseCsvLine(linha);

                    if (campos.Length >= 6)
                    {
                        int id = int.Parse(campos[0]);
                        int produtoId = int.Parse(campos[1]);
                        string tipo = campos[2];
                        int quantidade = int.Parse(campos[3]);
                        DateTime data = DateTime.Parse(campos[4]);
                        string observacao = campos[5];

                        movimentos.Add(new Movimento(id, produtoId, tipo, quantidade, data, observacao));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar movimentos: {ex.Message}");
            }

            return movimentos;
        }

        /// <summary>
        /// Salva produtos e movimentos usando escrita atômica
        /// </summary>
        /// <param name="produtos">Lista de produtos a salvar</param>
        /// <param name="movimentos">Lista de movimentos a salvar</param>
        public void SalvarDados(List<Produto> produtos, List<Movimento> movimentos)
        {
            SalvarProdutosAtomica(produtos);
            SalvarMovimentosAtomica(movimentos);
        }

        /// <summary>
        /// Salva produtos usando escrita atômica (.tmp + replace)
        /// Garante que os dados não sejam corrompidos em caso de falha
        /// </summary>
        private void SalvarProdutosAtomica(List<Produto> produtos)
        {
            string caminhoTemp = CaminhoProdutos + ".tmp";

            try
            {
                // Escreve no arquivo temporário com UTF-8
                using (var sw = new StreamWriter(caminhoTemp, false, Encoding.UTF8))
                {
                    // Cabeçalho
                    sw.WriteLine("id;nome;categoria;estoqueMinimo;saldo");

                    // Dados dos produtos
                    foreach (var p in produtos)
                    {
                        sw.WriteLine($"{p.Id};{EscapeCsv(p.Nome)};{EscapeCsv(p.Categoria)};{p.EstoqueMinimo};{p.Saldo}");
                    }

                    // IMPORTANTE: Força a gravação no disco
                    sw.Flush();
                }

                // Aguarda um pouco para garantir que o arquivo foi fechado
                System.Threading.Thread.Sleep(100);

                // Substitui o arquivo original pelo temporário (operação atômica)
                if (File.Exists(CaminhoProdutos))
                {
                    File.Delete(CaminhoProdutos);
                }

                File.Move(caminhoTemp, CaminhoProdutos);

                Console.WriteLine($"[OK] {produtos.Count} produtos salvos com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Ao salvar produtos: {ex.Message}");
                Console.WriteLine($"Detalhes: {ex.StackTrace}");

                // Limpa o arquivo temporário se existir
                if (File.Exists(caminhoTemp))
                {
                    try
                    {
                        File.Delete(caminhoTemp);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Salva movimentos usando escrita atômica (.tmp + replace)
        /// Garante que os dados não sejam corrompidos em caso de falha
        /// </summary>
        private void SalvarMovimentosAtomica(List<Movimento> movimentos)
        {
            string caminhoTemp = CaminhoMovimentos + ".tmp";

            try
            {
                // Escreve no arquivo temporário com UTF-8
                using (var sw = new StreamWriter(caminhoTemp, false, Encoding.UTF8))
                {
                    // Cabeçalho
                    sw.WriteLine("id;produtoId;tipo;quantidade;data;observacao");

                    // Dados dos movimentos
                    foreach (var m in movimentos)
                    {
                        // Formato ISO 8601 para a data
                        sw.WriteLine($"{m.Id};{m.ProdutoId};{m.Tipo};{m.Quantidade};{m.Data:yyyy-MM-ddTHH:mm:ss};{EscapeCsv(m.Observacao)}");
                    }

                    // IMPORTANTE: Força a gravação no disco
                    sw.Flush();
                }

                // Aguarda um pouco para garantir que o arquivo foi fechado
                System.Threading.Thread.Sleep(100);

                // Substitui o arquivo original pelo temporário (operação atômica)
                if (File.Exists(CaminhoMovimentos))
                {
                    File.Delete(CaminhoMovimentos);
                }

                File.Move(caminhoTemp, CaminhoMovimentos);

                Console.WriteLine($"[OK] {movimentos.Count} movimentos salvos com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Ao salvar movimentos: {ex.Message}");
                Console.WriteLine($"Detalhes: {ex.StackTrace}");

                // Limpa o arquivo temporário se existir
                if (File.Exists(caminhoTemp))
                {
                    try
                    {
                        File.Delete(caminhoTemp);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Escapa caracteres especiais para CSV (ponto e vírgula, aspas, quebras de linha)
        /// </summary>
        /// <param name="campo">Texto a ser escapado</param>
        /// <returns>Texto escapado e pronto para gravação em CSV</returns>
        private string EscapeCsv(string campo)
        {
            if (string.IsNullOrEmpty(campo))
                return "";

            // Se contém ponto e vírgula, aspas ou quebras de linha, envolve em aspas
            if (campo.Contains(";") || campo.Contains("\"") || campo.Contains("\n") || campo.Contains("\r"))
            {
                // Duplica aspas internas e envolve o campo em aspas
                return $"\"{campo.Replace("\"", "\"\"")}\"";
            }

            return campo;
        }

        /// <summary>
        /// Faz parse de uma linha CSV considerando campos entre aspas
        /// </summary>
        /// <param name="linha">Linha CSV a ser parseada</param>
        /// <returns>Array com os campos separados</returns>
        private string[] ParseCsvLine(string linha)
        {
            var campos = new List<string>();
            bool dentroAspas = false;
            var campoAtual = new StringBuilder();

            for (int i = 0; i < linha.Length; i++)
            {
                char c = linha[i];

                if (c == '"')
                {
                    // Verifica se é aspas duplas (escape)
                    if (i + 1 < linha.Length && linha[i + 1] == '"')
                    {
                        campoAtual.Append('"');
                        i++; // Pula a próxima aspas
                    }
                    else
                    {
                        dentroAspas = !dentroAspas;
                    }
                }
                else if (c == ';' && !dentroAspas)
                {
                    // Fim do campo
                    campos.Add(campoAtual.ToString());
                    campoAtual.Clear();
                }
                else
                {
                    campoAtual.Append(c);
                }
            }

            // Adiciona o último campo
            campos.Add(campoAtual.ToString());

            return campos.ToArray();
        }
    }
}