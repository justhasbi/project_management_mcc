using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{

    [Table("tb_m_account")]
    public class Account
    {

        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
