namespace ContactManager.Domain.Utilities;


public static class ContactBusinessConfiguration
{
    public const int FIRST_NAME_MIN_LENGTH = 2;
    public const int FIRST_NAME_MAX_LENGTH = 50;

    public const int LAST_NAME_MIN_LENGTH = 2;
    public const int LAST_NAME_MAX_LENGTH = 50;

    public const int ADDRESS_MIN_LENGTH = 5;
    public const int ADDRESS_MAX_LENGTH = 250;

    public const int PHONE_MIN_LENGTH = 7;
    public const int PHONE_MAX_LENGTH = 15;

    public const int IBAN_MIN_LENGTH = 15;
    public const int IBAN_MAX_LENGTH = 34;
}