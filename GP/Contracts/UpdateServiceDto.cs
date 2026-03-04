namespace GP.Contracts
{
    /// <summary>
    /// DTO dla edycji istniejącej usługi.
    /// Frontend wysyła TO do API.
    /// </summary>
    public record UpdateServiceDto(
        string Name,
        string Description,
        decimal PriceFrom,
        bool IsActive
    );
}
