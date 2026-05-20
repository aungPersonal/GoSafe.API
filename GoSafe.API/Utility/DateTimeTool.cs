namespace GoSafe.API.Utility
{
    public static class DateTimeTool
    {

        public static DateTime ConvertIntoMyanTime(DateTime dt)
        {
            TimeZoneInfo myanmarTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(dt, myanmarTimeZone);
        }
    }
}
