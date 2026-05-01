namespace ContactManager.Features.Models.Requests;

public sealed record CreateContactRequest(
    string FirstName,
    string Surname,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    string IBAN);
