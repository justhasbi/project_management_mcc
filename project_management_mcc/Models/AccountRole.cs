using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_tr_account_role")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }

        // relation
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
