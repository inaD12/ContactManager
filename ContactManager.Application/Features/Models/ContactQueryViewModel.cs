namespace ContactManager.Application.Features.Models
{
	public sealed record ContactQueryViewModel(
		string Id,
		string FirstName,
		string Surname,
		DateOnly DateOfBirth,
		string Address,
		string PhoneNumber,
		string IBAN);
}