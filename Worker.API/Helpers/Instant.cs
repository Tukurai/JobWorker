namespace Worker.API.Helpers;

public static class Instant
{
    public static DateTime DateTimeNow
    {
        get
        {
            return DateTime.UtcNow;
        }
    }

    public static DateOnly DateNow
    {
        get
        {
            var now = DateTimeNow;
            return new DateOnly(now.Year, now.Month, now.Day);
        }
    }

    public static TimeOnly TimeNow
    {
        get
        {
            var now = DateTimeNow;
            return new TimeOnly(now.Hour, now.Minute, now.Second);
        }
    }
}
