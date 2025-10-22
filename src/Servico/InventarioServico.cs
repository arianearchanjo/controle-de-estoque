using System;
using System.Collections.Generic;
using controle_de_estoque_ub.src.Modelo;

namespace controle_de_estoque_ub.src.Servico
{
    public class InventarioServico
    {
        private List<Produto> produtos = new();
        private List<Movimento> movimentos = new();

        public InventarioServico()
        {
            // placeholder: normalmente carregaria dados do CSV
        }

        public void ListarProdutos()
        {
            Console.WriteLine("Simulação: Listagem de produtos...");
        }

        public void CadastrarProduto()
        {
            Console.WriteLine("Simulação: Cadastro de produto...");
        }

        public void EditarProduto()
        {
            Console.WriteLine("Simulação: Edição de produto...");
        }

        public void ExcluirProduto()
        {
            Console.WriteLine("Simulação: Exclusão de produto...");
        }

        public void EntradaEstoque()
        {
            Console.WriteLine("Simulação: Entrada de estoque...");
        }

        public void SaidaEstoque()
        {
            Console.WriteLine("Simulação: Saída de estoque...");
        }

        public void RelatorioAbaixoMinimo()
        {
            Console.WriteLine("Simulação: Relatório de produtos abaixo do mínimo...");
        }

        public void ExtratoPorProduto()
        {
            Console.WriteLine("Simulação: Extrato de movimentos por produto...");
        }

        public void SalvarDados()
        {
            Console.WriteLine("Simulação: Salvando dados...");
        }
    }
}
