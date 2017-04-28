namespace DateTimeProvider
{
    public interface IDateTimeProvider
    {
        System.DateTimeOffset Now { get; }
    }
}