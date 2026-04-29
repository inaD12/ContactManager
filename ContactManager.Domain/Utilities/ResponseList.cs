using System.Net;
using ContactManager.Domain.Results;

namespace ContactManager.Domain.Utilities;

public static class ResponseList
{
    // --Success--
    public static Response ContactCreated =>
        Response.Create("Contact successfully created", HttpStatusCode.Created);

    public static Response ContactUpdated =>
        Response.Create("Contact successfully updated", HttpStatusCode.OK);

    public static Response ContactDeleted =>
        Response.Create("Contact successfully deleted", HttpStatusCode.OK);

    // --Errors--
    
    // FirstName
    public static Response FirstNameRequired =>
        Response.Create("First name is required", HttpStatusCode.BadRequest);

    public static Response FirstNameTooLong =>
        Response.Create("First name cannot exceed 100 characters", HttpStatusCode.BadRequest);

    // Surname
    public static Response SurnameRequired =>
        Response.Create("Surname is required", HttpStatusCode.BadRequest);

    // DateOfBirth
    public static Response DateOfBirthInFuture =>
        Response.Create("Date of birth cannot be in the future", HttpStatusCode.BadRequest);

    public static Response DateOfBirthTooOld =>
        Response.Create("Date of birth is not valid", HttpStatusCode.BadRequest);

    // Address
    public static Response AddressRequired =>
        Response.Create("Address is required", HttpStatusCode.BadRequest);

    public static Response AddressTooLong =>
        Response.Create("Address cannot exceed 250 characters", HttpStatusCode.BadRequest);

    // Phone
    public static Response PhoneNumberRequired =>
        Response.Create("Phone number is required", HttpStatusCode.BadRequest);

    public static Response PhoneNumberInvalid =>
        Response.Create("Phone number format is invalid", HttpStatusCode.BadRequest);
    
    public static Response PhoneNumberAlreadyExists =>
        Response.Create("This phone number is already in use", HttpStatusCode.Conflict);

    // IBAN
    public static Response IBANRequired =>
        Response.Create("IBAN is required", HttpStatusCode.BadRequest);

    public static Response IBANInvalid =>
        Response.Create("IBAN format is invalid", HttpStatusCode.BadRequest);
    
    public static Response IBANAlreadyExists =>
        Response.Create("This IBAN is already in use", HttpStatusCode.Conflict);
}