using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSUBWorkFlowProject.Shared.Models
{
    public partial class Login
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }
        [Key]
        public string username { get; set; }

        public string password { get; set; }
        public bool isManager { get; set; }
    }
}
