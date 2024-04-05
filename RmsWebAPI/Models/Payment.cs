namespace RmsWebAPI.Models
{
    public class Payment
    {
        public byte[]? PaymentSlip_file { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}
