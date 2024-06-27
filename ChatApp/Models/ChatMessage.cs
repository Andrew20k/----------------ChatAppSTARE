using System;

namespace ChatApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; } = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

        public string FormattedCreatedOn => CreatedOn.ToString("ddd 'o' HH:mm");
    }
}
