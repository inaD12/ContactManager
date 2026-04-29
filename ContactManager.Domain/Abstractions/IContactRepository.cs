using ContactManager.Domain.Entities;

namespace ContactManager.Domain.Abstractions;

public interface IContactRepository
{
    Task AddAsync(Contact contact, CancellationToken cancellationToken = default);
    Task<bool> ExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIBANAsync(string iban, CancellationToken cancellationToken = default);
}