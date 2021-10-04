using System.ComponentModel.DataAnnotations;

namespace project_management_mcc.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [StringLength(40, ErrorMessage = "First name must be between 3 and 40 characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [StringLength(40, ErrorMessage = "First name must be between 3 and 40 characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(40, ErrorMessage = "Email must be between 10 and 40 characters", MinimumLength = 10)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password must be between 5 and 50 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        public int Gender { get; set; }

        public int? DepartmentId { get; set; }

        public int? JobId { get; set; }
    }
}
