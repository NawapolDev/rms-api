namespace RmsWebAPI.Models
{
    public class User
    {
        public Guid U_id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Idcard { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Agency { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Subdistrict { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Active { get; set; } = string.Empty;
        public int Role_id { get; set; }
        public DateTime Createdate { get; set; }
        public string Createby { get; set; } = string.Empty;
        public DateTime? Modifieddate { get; set; }
        public string? Modifiedby { get; set; } = string.Empty;
    }
}
