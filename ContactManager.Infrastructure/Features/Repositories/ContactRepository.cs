using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Features.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Features.Repositories;

public class ContactRepository(ContactsDbContext context)
    : IContactRepository
{
    public async Task<Contact?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var contact = await context.Contacts
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return contact;
    }

    public async Task AddAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        await context.Contacts.AddAsync(contact, cancellationToken);
    }

    public void Delete(Contact contact)
    {
        context.Contacts.Remove(contact);
    }

    public async Task<bool> ExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        return await context.Contacts
            .AnyAsync(x => x.PhoneNumber.Value == phone, cancellationToken);
    }

    public async Task<bool> ExistsByIBANAsync(string iban, CancellationToken cancellationToken = default)
    {
        return await context.Contacts
            .AnyAsync(x => x.IBAN.Value == iban, cancellationToken);
    }
}