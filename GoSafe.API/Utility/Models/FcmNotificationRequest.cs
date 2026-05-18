namespace GoSafe.API.Utility.Models
{
    public class FcmNotificationRequest
    {
        public string Topic { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string DetailPage { get; set; } = string.Empty;
        public long NotificationId { get; set; }
        public int Type { get; set; } = 0;
        public string? Attribute1 { get; set; } = string.Empty;
        public string? Attribute2 { get; set; } = string.Empty;
        public string? Attribute3 { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
    }
}
