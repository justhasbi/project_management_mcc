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

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(40, ErrorMessage = "Email must be between 10 and 40 characters", MinimumLength = 10)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum eight characters, at least one letter and one number")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*)[A-Za-z]{8,}$")]
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
