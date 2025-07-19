
namespace Ordering.Application.Dtos;
public record AddressDto(
    string FirstName,
    string LastName,
    string AddressLine,
    string Email,
    string Country,
    string State,
    string ZipCode
);


