namespace RmsWebAPI.Models
{
    public class Reservation
    {
        public Guid Rsv_id { get; set; } = Guid.Empty;
        public Guid mb_id { get; set; } = Guid.Empty;
        public Guid Room_id { get; set; } = Guid.Empty;
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Totalprice { get; set; }
        public string Approve { get; set; } = string.Empty;
        public string Approveby { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public Byte? PaymentSlip_file { get; set; }
        public string PaymentSlip_url { get; set; } = string.Empty;
    }
}
