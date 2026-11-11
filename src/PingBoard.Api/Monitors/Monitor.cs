namespace PingBoard.Api.Monitors;

public sealed class Monitor
{
    public const int NameMaxLength = 100;
    public const int MinIntervalSeconds = 60;
    public const int MaxIntervalSeconds = 3600;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public int IntervalSeconds { get; private set; }
    public bool IsActive { get; private set; }
    public MonitorStatus CurrentStatus { get; private set; } = MonitorStatus.Unknown;
    public DateTimeOffset CreatedAt { get; private set; }

    private Monitor() { }

    public static Monitor Create(string name, string url, int intervalSeconds, DateTimeOffset now)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (name.Length > NameMaxLength)
            throw new ArgumentException($"Name must be {NameMaxLength} characters or less.", nameof(name));

        if (!IsHttpUrl(url))
            throw new ArgumentException("Url must be an absolute http or https URL.", nameof(url));

        if (intervalSeconds < MinIntervalSeconds || intervalSeconds > MaxIntervalSeconds)
            throw new ArgumentException(
                $"Interval must be between {MinIntervalSeconds} and {MaxIntervalSeconds} seconds.",
                nameof(intervalSeconds));

        return new Monitor
        {
            Id = Guid.NewGuid(),
            Name = name,
            Url = url,
            IntervalSeconds = intervalSeconds,
            IsActive = true,
            CurrentStatus = MonitorStatus.Unknown,
            CreatedAt = now
        };
    }

    private static bool IsHttpUrl(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out var uri)
        && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}