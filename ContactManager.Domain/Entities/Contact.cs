using ContactManager.Domain.Entities.Base;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Domain.Entities;

public class Contact : BaseEntity
{
    public FirstName FirstName { get; private set; }
    public Surname Surname { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IBAN IBAN { get; private set; }

    private Contact() { }

    private Contact(
        FirstName firstName,
        Surname surname,
        DateOfBirth dateOfBirth,
        Address address,
        PhoneNumber phoneNumber,
        IBAN iban)
    {
        FirstName = firstName;
        Surname = surname;
        DateOfBirth = dateOfBirth;
        Address = address;
        PhoneNumber = phoneNumber;
        IBAN = iban;
    }

    public static Result<Contact> Create(
        FirstName firstName,
        Surname surname,
        DateOfBirth dateOfBirth,
        Address address,
        PhoneNumber phoneNumber,
        IBAN iban)
    {
        var contact = new Contact(
            firstName,
            surname,
            dateOfBirth,
            address,
            phoneNumber,
            iban
        );

        return Result<Contact>.Success(contact, ResponseList.ContactCreated);
    }
    
    public Result ChangeAddress(Address newAddress)
    {
        Address = newAddress;
        return Result.Success(ResponseList.ContactUpdated);
    }
    
    public Result ChangePhoneNumber(PhoneNumber newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
        return Result.Success(ResponseList.ContactUpdated);
    }
    
    public Result ChangeIBAN(IBAN newIBAN)
    {
        IBAN = newIBAN;
        return Result.Success(ResponseList.ContactUpdated);
    }
}