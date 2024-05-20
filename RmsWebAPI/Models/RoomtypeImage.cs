namespace RmsWebAPI.Models
{
    public class RoomTypeImage
    {
        public byte[]? Image_file { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}
