using ContactManager.Application.Features.Models;
using ContactManager.Application.Features.Sorting;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Features.Abstractions;

public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task AddAsync(Contact contact, CancellationToken cancellationToken = default);
    void Delete(Contact contact);
    Task<bool> ExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIBANAsync(string iban, CancellationToken cancellationToken = default);
    Task<PagedList<Contact>> GetAllAsync(ContactFilter query, CancellationToken cancellationToken = default);
}