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
            int[] datePart = Array.ConvertAll(dateString.Split('-'), int.Parse);
            DateTime dt = new DateTime(datePart[0], datePart[1], datePart[2], datePart[3], datePart[4], datePart[5]);
            Value = dt;
        }
    }
}
