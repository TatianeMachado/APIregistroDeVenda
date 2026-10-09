using APIregistroDeVenda.DTO;

namespace APIregistroDeVenda.Service;

public class ComissaoService
{
    public List<ComissaoVendedorResponse> Calcular(
        ComissaoCalcularRequest request)
    {
        return request.Vendas
            .GroupBy(venda => venda.Vendedor!)
            .Select(grupo => new ComissaoVendedorResponse
            {
                Vendedor = grupo.Key,

                QuantidadeVendas = grupo.Count(),

                TotalVendas = grupo.Sum(
                    venda => venda.Valor!.Value),

                TotalComissao = grupo.Sum(
                    venda => CalcularComissao(venda.Valor!.Value))
            })
            .ToList();
    }

    private static decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 100m)
        {
            return 0m;
        }

        if (valorVenda < 500m)
        {
            return valorVenda * 0.01m;
        }

        return valorVenda * 0.05m;
    }
}