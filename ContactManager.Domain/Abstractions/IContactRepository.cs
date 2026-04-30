using ContactManager.Domain.Entities;

namespace ContactManager.Domain.Abstractions;

public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task AddAsync(Contact contact, CancellationToken cancellationToken = default);
    void Delete(Contact contact);
    Task<bool> ExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIBANAsync(string iban, CancellationToken cancellationToken = default);
}