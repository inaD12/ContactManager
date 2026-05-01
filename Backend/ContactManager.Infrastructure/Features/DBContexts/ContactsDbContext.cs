using ContactManager.Domain.Entities;
using ContactManager.Infrastructure.Features.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Features.DBContexts;

public sealed class ContactsDbContext: DbContext
{
	public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
		: base(options)
	{ }
	
	public DbSet<Contact> Contacts => Set<Contact>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ContactConfiguration());
	}
}
