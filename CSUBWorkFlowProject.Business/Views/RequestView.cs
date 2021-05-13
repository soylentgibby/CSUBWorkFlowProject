using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSUBWorkFlowProject.Shared.Views
{
    public partial class RequestView
    {
        public int RequestID { get; set; }
        public string RequestField { get; set; }
        public string RequestChange { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestBlob { get; set; }
        public string UserName { get; set; }
        public bool isApproved { get; set; }
        public bool isDenied { get; set; }
        public DateTime ResponseDate { get; set; }
        public string ManagerName { get; set; }
    }
}
