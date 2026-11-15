using Microsoft.Extensions.Logging;

namespace XWiki.Api.Test.Offline;

/// <summary>
/// A logger that keeps every formatted message together with its level.
/// </summary>
internal sealed class CapturingLogger : ILogger
{
	/// <summary>
	/// Gets the captured entries in the order they were written.
	/// </summary>
	public List<(LogLevel Level, string Message)> Entries { get; } = [];

	/// <inheritdoc/>
	public IDisposable? BeginScope<TState>(TState state) where TState : notnull
		=> null;

	/// <inheritdoc/>
	public bool IsEnabled(LogLevel logLevel)
		=> true;

	/// <inheritdoc/>
	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		=> Entries.Add((logLevel, formatter(state, exception)));
}
