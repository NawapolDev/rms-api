namespace RmsWebAPI.Models
{
    public class Role
    {
        public int Role_id { get; set; }
        public string Role_name { get; set; } = string.Empty;
        public string Active { get; set; } = string.Empty;
        public DateTime Createdate { get; set; }
        public string Createby { get; set; } = string.Empty;
    }
}
