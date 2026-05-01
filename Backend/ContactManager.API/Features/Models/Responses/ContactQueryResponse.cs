namespace ContactManager.Features.Models.Responses;

public sealed record ContactQueryResponse(
	string Id,
	string FirstName,
	string Surname,
	DateOnly DateOfBirth,
	string Address,
	string PhoneNumber,
	string IBAN);