namespace Api.Utils;

/// <summary>
/// We need a reliable and universal clock.
/// </summary>
public interface IClock
{
    DateTime Now { get; }
}

public class LocalClock : IClock
{
    public DateTime Now => DateTime.Now;
}
