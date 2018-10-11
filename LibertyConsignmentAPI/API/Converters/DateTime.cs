using System;

namespace LibertyConsignmentAPI.Converters
{
    public class LibertyDate
    {
        public DateTime Value { get; private set; }

        public LibertyDate(string dateString)
        {
            dateString = dateString.Replace("{", "");
            dateString = dateString.Replace("}", "");
            int yearPart = int.Parse(dateString.Substring(0, 4));
            int monthPart = int.Parse(dateString.Substring(5, 2));
            int dayPart = int.Parse(dateString.Substring(8, 2));
            int hourPart = int.Parse(dateString.Substring(11, 2));
            int minutePart = int.Parse(dateString.Substring(14, 2));
            int secondPart = int.Parse(dateString.Substring(17, 2));
            DateTime dt = new DateTime(yearPart, monthPart, dayPart, hourPart, minutePart, secondPart);
            Value = dt;
        }
    }
}
