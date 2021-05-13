using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSUBWorkFlowProject.Shared.Models
{
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }
        [Key]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public bool isManager { get; set; }
    }
}
