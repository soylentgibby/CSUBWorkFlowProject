using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSUBWorkFlowProject.Shared.Models
{
    public partial class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }
        public string RequestField { get; set; }
        public string RequestChange { get; set; }
        public DateTime RequestDate { get; set; }
        public string OldRequestBlob { get; set; }
        public string NewRequestBlob { get; set; }
        public int UserId { get; set; }
        public bool isApproved { get; set; }
        public bool isDenied { get; set; }
        public DateTime ResponseDate { get; set; }
        public int ManagerUserId { get; set; }
    }
}
