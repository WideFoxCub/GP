namespace GP.Contracts
{
    public record ServiceDto(
        int Id,
        string Name,
        string Description,
        decimal PriceFrom
    );
}
