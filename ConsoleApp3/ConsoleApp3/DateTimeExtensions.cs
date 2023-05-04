using WeatherApp;

public static class DateTimeExtensions
{
    public static DateTime GetDateTimeFromUnixTime(this ForecastItem item)
    {
        double unixTimeStamp = item.DateTimeUnix;
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }





}
