namespace Identity.Service.EventHandlers.Responses;

public record IdentityAccess
{
    public bool Succeeded { get; set; }
    public string? AccessToken { get; set; }
}
