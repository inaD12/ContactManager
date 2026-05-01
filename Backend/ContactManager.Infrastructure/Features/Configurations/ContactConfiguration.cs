using ContactManager.Domain.Entities;
using ContactManager.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Infrastructure.Features.Configurations;

internal sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
	public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(x => x.FirstName, firstName =>
        {
            firstName.Property(x => x.Value)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(ContactBusinessConfiguration.FIRST_NAME_MAX_LENGTH);
        });

        builder.OwnsOne(x => x.Surname, surname =>
        {
            surname.Property(x => x.Value)
                .HasColumnName("surname")
                .IsRequired()
                .HasMaxLength(ContactBusinessConfiguration.LAST_NAME_MAX_LENGTH);
        });

        builder.OwnsOne(x => x.DateOfBirth, dob =>
        {
            dob.Property(x => x.Value)
                .HasColumnName("date_of_birth")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Value)
                .HasColumnName("address")
                .IsRequired()
                .HasMaxLength(ContactBusinessConfiguration.ADDRESS_MAX_LENGTH);
        });

        builder.OwnsOne(x => x.PhoneNumber, phone =>
        {
            phone.Property(x => x.Value)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(ContactBusinessConfiguration.PHONE_MAX_LENGTH);

            phone.HasIndex(x => x.Value)
                .IsUnique();
        });

        builder.OwnsOne(x => x.IBAN, iban =>
        {
            iban.Property(x => x.Value)
                .HasColumnName("iban")
                .IsRequired()
                .HasMaxLength(ContactBusinessConfiguration.IBAN_MAX_LENGTH);

            iban.HasIndex(x => x.Value)
                .IsUnique();
        });
    }
}

