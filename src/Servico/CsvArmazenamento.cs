using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using controle_de_estoque_ub.src.Modelo;

namespace controle_de_estoque_ub.src.Servico
{
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
        }

        /// <summary>
        /// Carrega produtos e movimentos dos arquivos CSV
        /// </summary>
        public (List<Produto>, List<Movimento>) CarregarDados()
        {
            var produtos = CarregarProdutos();
            var movimentos = CarregarMovimentos();
            return (produtos, movimentos);
        }

        /// <summary>
        /// Carrega produtos do CSV
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
                var linhas = File.ReadAllLines(CaminhoProdutos);

                // Pula o cabeçalho (primeira linha)
                for (int i = 1; i < linhas.Length; i++)
                {
                    var linha = linhas[i].Trim();
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
        /// Carrega movimentos do CSV
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
                var linhas = File.ReadAllLines(CaminhoMovimentos);

                // Pula o cabeçalho
                for (int i = 1; i < linhas.Length; i++)
                {
                    var linha = linhas[i].Trim();
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
        public void SalvarDados(List<Produto> produtos, List<Movimento> movimentos)
        {
            SalvarProdutosAtomica(produtos);
            SalvarMovimentosAtomica(movimentos);
        }

        /// <summary>
        /// Salva produtos usando escrita atômica (.tmp + replace)
        /// </summary>
        private void SalvarProdutosAtomica(List<Produto> produtos)
        {
            string caminhoTemp = CaminhoProdutos + ".tmp";

            try
            {
                // Escreve no arquivo temporário
                using (var sw = new StreamWriter(caminhoTemp, false))
                {
                    // Cabeçalho
                    sw.WriteLine("id;nome;categoria;estoqueMinimo;saldo");

                    // Dados
                    foreach (var p in produtos)
                    {
                        sw.WriteLine($"{p.Id};{EscapeCsv(p.Nome)};{EscapeCsv(p.Categoria)};{p.EstoqueMinimo};{p.Saldo}");
                    }
                }

                // Substitui o arquivo original pelo temporário (operação atômica)
                if (File.Exists(CaminhoProdutos))
                {
                    File.Delete(CaminhoProdutos);
                }
                File.Move(caminhoTemp, CaminhoProdutos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar produtos: {ex.Message}");

                // Limpa o arquivo temporário se existir
                if (File.Exists(caminhoTemp))
                {
                    File.Delete(caminhoTemp);
                }
            }
        }

        /// <summary>
        /// Salva movimentos usando escrita atômica (.tmp + replace)
        /// </summary>
        private void SalvarMovimentosAtomica(List<Movimento> movimentos)
        {
            string caminhoTemp = CaminhoMovimentos + ".tmp";

            try
            {
                // Escreve no arquivo temporário
                using (var sw = new StreamWriter(caminhoTemp, false))
                {
                    // Cabeçalho
                    sw.WriteLine("id;produtoId;tipo;quantidade;data;observacao");

                    // Dados
                    foreach (var m in movimentos)
                    {
                        sw.WriteLine($"{m.Id};{m.ProdutoId};{m.Tipo};{m.Quantidade};{m.Data:o};{EscapeCsv(m.Observacao)}");
                    }
                }

                // Substitui o arquivo original pelo temporário (operação atômica)
                if (File.Exists(CaminhoMovimentos))
                {
                    File.Delete(CaminhoMovimentos);
                }
                File.Move(caminhoTemp, CaminhoMovimentos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar movimentos: {ex.Message}");

                // Limpa o arquivo temporário se existir
                if (File.Exists(caminhoTemp))
                {
                    File.Delete(caminhoTemp);
                }
            }
        }

        /// <summary>
        /// Escapa caracteres especiais para CSV (ponto e vírgula, aspas, quebras)
        /// </summary>
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
        private string[] ParseCsvLine(string linha)
        {
            var campos = new List<string>();
            bool dentroAspas = false;
            var campoAtual = "";

            for (int i = 0; i < linha.Length; i++)
            {
                char c = linha[i];

                if (c == '"')
                {
                    // Verifica se é aspas duplas (escape)
                    if (i + 1 < linha.Length && linha[i + 1] == '"')
                    {
                        campoAtual += '"';
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
                    campos.Add(campoAtual);
                    campoAtual = "";
                }
                else
                {
                    campoAtual += c;
                }
            }

            // Adiciona o último campo
            campos.Add(campoAtual);

            return campos.ToArray();
        }
    }
}