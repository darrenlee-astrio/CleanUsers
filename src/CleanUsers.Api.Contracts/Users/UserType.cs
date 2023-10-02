using System.Text.Json.Serialization;

namespace CleanUsers.Api.Contracts.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    Gold,
    Silver,
    Bronze
}
