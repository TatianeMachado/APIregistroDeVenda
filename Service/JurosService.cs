namespace APIregistroDeVenda.Service;

public class JurosService
{
    public decimal Calcular(
        decimal valor,
        DateOnly dataVencimento)
    {
        var fuso = TimeZoneInfo.FindSystemTimeZoneById(
            "America/Sao_Paulo");

        var agora = TimeZoneInfo.ConvertTime(
            DateTimeOffset.UtcNow,
            fuso);

        var hoje = DateOnly.FromDateTime(agora.DateTime);

        int diasAtraso = hoje.DayNumber - dataVencimento.DayNumber;

        if (diasAtraso <= 0)
        {
            return 0m;
        }

        return valor * 0.025m * diasAtraso;
    }
}