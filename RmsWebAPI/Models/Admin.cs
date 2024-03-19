namespace RmsWebAPI.Models
{
    public class Admin
    {
        public Guid adm_id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Active { get; set; } = string.Empty;
        public DateTime Createdate { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string? Modifiedby { get; set; } = string.Empty;

    }
}
