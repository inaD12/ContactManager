using System.ComponentModel.DataAnnotations;

namespace ContactManager.Domain.Options;

public sealed class DatabaseOptions
{
	[Required]
	public string ConnectionString { get; set; } = string.Empty;
}
