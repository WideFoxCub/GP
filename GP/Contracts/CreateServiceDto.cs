namespace GP.Contracts
{
    /// <summary>
    /// DTO dla tworzenia nowej usługi.
    /// Frontend wysyła TO do API.
    /// </summary>
    public record CreateServiceDto(
        string Name,
        string Description,
        decimal PriceFrom
    );
}
