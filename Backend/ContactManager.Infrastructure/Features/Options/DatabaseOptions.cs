using System.ComponentModel.DataAnnotations;

namespace ContactManager.Infrastructure.Features.Options;

public sealed class DatabaseOptions
{
	[Required]
	public string ConnectionString { get; set; } = string.Empty;
}
