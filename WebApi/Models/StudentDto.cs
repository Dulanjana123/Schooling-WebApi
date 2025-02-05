using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string NIC { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public string? ProfileImageBase64 { get; set; }
    }

    // Base DTO to avoid code duplication
    public class BaseStudentDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "NIC is required.")]
        [StringLength(12, ErrorMessage = "NIC must be either 10 or 12 characters.")]
        public string NIC { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [CustomValidation(typeof(StudentValidation), nameof(StudentValidation.ValidateDateOfBirth))]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; }

        public bool Active { get; set; }
    }

    // CreateStudentDto inherits BaseStudentDto
    public class CreateStudentDto : BaseStudentDto
    {
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Only .jpg and .png files are allowed.")]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "Profile image must be less than 2MB.")]
        public IFormFile? Image { get; set; }
    }

    // UpdateStudentDto inherits BaseStudentDto and adds Id
    public class UpdateStudentDto : BaseStudentDto
    {
        [Required]
        public int Id { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Only .jpg and .png files are allowed.")]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "Profile image must be less than 2MB.")]
        public IFormFile? Image { get; set; }
    }
}
