using System;

namespace Package.Extensions
{
    public static class DirectoryExtensions
    {
        public static string GenerateDirectory()
        {
            var year = DateTimeOffset.Now.Year;
            var month = DateTimeOffset.Now.Month.ToString();
            month = month.Length == 1 ? "0" + month : month;
            var day = DateTimeOffset.Now.Day.ToString();
            day = day.Length == 1 ? "0" + day : day;
            var hour = DateTimeOffset.Now.Hour.ToString();
            hour = hour.Length == 1 ? "0" + hour : hour;
            return $"{year}\\{month}\\{day}\\{hour}";
        }
    }
}