using ContactManager.Application.Features.Abstractions;
using ContactManager.Application.Features.Models;
using ContactManager.Application.Features.Sorting;
using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Extensions;
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

    public async Task<PagedList<Contact>> GetAllAsync(
        ContactFilter query,
        CancellationToken cancellationToken = default)
    {
        var entitiesQuery = context.Contacts
            .Where(r =>
                (string.IsNullOrEmpty(query.FirstName) ||
                 EF.Functions.ILike(r.FirstName.Value, $"%{query.FirstName}%")) &&

                (string.IsNullOrEmpty(query.Surname) ||
                 EF.Functions.ILike(r.Surname.Value, $"%{query.Surname}%")) &&

                (!query.MinDateOfBirth.HasValue ||
                 r.DateOfBirth.Value >= query.MinDateOfBirth.Value) &&

                (!query.MaxDateOfBirth.HasValue ||
                 r.DateOfBirth.Value <= query.MaxDateOfBirth.Value) &&

                (string.IsNullOrEmpty(query.Address) ||
                 EF.Functions.ILike(r.Address.Value, $"%{query.Address}%")) &&

                (string.IsNullOrEmpty(query.PhoneNumber) ||
                 EF.Functions.ILike(r.PhoneNumber.Value, $"%{query.PhoneNumber}%"))
            );


        entitiesQuery = entitiesQuery.ApplySorting(query.SortBy, query.SortOrder);

        return await PagedList<Contact>.CreateAsync(
            entitiesQuery,
            query.Page,
            query.PageSize,
            cancellationToken
        );
    }
}