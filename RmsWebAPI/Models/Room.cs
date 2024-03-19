namespace RmsWebAPI.Models
{
    public class Room
    {
        public Guid Room_id { get; set; } = Guid.Empty;
        public string Room_name { get; set;} = string.Empty;
        public int Typeid { get; set; }
        public string Active { get; set; } = string.Empty;
        public DateTime Createdate { get; set; }
        public string Createby { get; set; } = string.Empty;
        public DateTime? Modifieddate { get; set; }
        public string? Modifiedby { get; set; } = string.Empty;
    }
}
