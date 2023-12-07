namespace DigitalWallet.Domain.Common.Outbox;

public sealed class OutboxMessage
{
    #region Ctor for migrations
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public OutboxMessage()
    {
    // For migrations only
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion

    private OutboxMessage(
        Guid id,
        DateTime occuredOnUtc,
        string type,
        string content)
    {
        Id = id;
        OccurredOnUtc = occuredOnUtc;
        Type = type;
        Content = content;
    }
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Content { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    public static OutboxMessage Create(
        Guid id,
        DateTime occuredOnUtc,
        string type,
        string content)
    {
        return new OutboxMessage(
            id,
            occuredOnUtc,
            type,
            content);
    }

    public void ProcessedAt(DateTime date)
    {
        ProcessedOnUtc = date;
    }

    public void CreateErrorMessage(string error)
    {
        Error = error;
    }
}
