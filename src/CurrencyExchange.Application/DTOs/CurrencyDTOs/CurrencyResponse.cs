namespace CurrencyExchange.Application.DTOs.CurrencyDTOs
{
    public record CurrencyResponse(Guid Id, string Code, string FullName, string Sign);
}
