namespace RmsWebAPI.Models
{
    public class RoomType
    {
        public int? Type_id { get; set; }
        public string Type_name { get; set; } = string.Empty;
        public int? Room_size { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public string Active { get; set; } = string.Empty;
        public DateTime? Createdate { get; set; }
        public string Createby { get; set; } = string.Empty;
        public DateTime? Modifieddate { get; set; }
        public string? Modifiedby { get; set; } = string.Empty;
        public byte[]? Image_file { get; set; }
        public IFormFile? File { get; set; }
    }
}
