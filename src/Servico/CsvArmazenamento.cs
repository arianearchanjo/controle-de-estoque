using System.Collections.Generic;
using controle_de_estoque_ub.src.Modelo;

namespace controle_de_estoque_ub.src.Servico
{
    public class CsvArmazenamento
    {
        public (List<Produto>, List<Movimento>) CarregarDados()
        {
            return (new List<Produto>(), new List<Movimento>());
        }

        public void SalvarDados(List<Produto> produtos, List<Movimento> movimentos)
        {
            // placeholder
        }
    }
}
