using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        public enum Gender
        {
            Male,
            Female
        }

        [Required]
        [Range(0,1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(40, ErrorMessage = "Email must be between 10 and 40 characters", MinimumLength = 10)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum eight characters, at least one letter and one number")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
        public string Password { get; set; }

        public int? DepartmentId { get; set; }

        public int? JobId { get; set; }

        public int? RoleId { get; set; }
    }
}
