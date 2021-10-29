using System;
using System.ComponentModel.DataAnnotations;

namespace CDRBIP.Modules.CallDetailRecordManagement.Domain
{
    public class CallDetailRecord
    {
        public long CallerId { get; set; }
        public string Recipient { get; set; }
        public DateTime CallDate { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public double Cost { get; set; }
        [Key]
        public string Reference { get; set; }
        public string Currency { get; set; }
        public CallType Type { get; set; }
    }

    public enum CallType
    {
        Domestic = 1,
        International
    }
}
