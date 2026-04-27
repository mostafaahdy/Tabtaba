using System.ComponentModel.DataAnnotations;
using Tabtaba.Domain.Enums;

namespace Tabtaba.Shared.DTOs.Therapist;
public class PersonalInfoDto
{
    
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Full name must be between 3 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    
    [Required(ErrorMessage = "Title is required.")]
    public Title Title { get; set; }

    
    [Required(ErrorMessage = "Gender is required.")]
    public Gender Gender { get; set; }

    
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Username must be between 3 and 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$",
        ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    
    [Required(ErrorMessage = "Mobile number is required.")]
    [RegularExpression(@"^\+?[1-9]\d{7,14}$",
        ErrorMessage = "Invalid mobile number format.")]
    public string MobileNumber { get; set; } = string.Empty;

    
    [Required(ErrorMessage = "Date of birth is required.")]
    public DateTime DateOfBirth { get; set; }

    
    [Required(ErrorMessage = "Nationality is required.")]
    [StringLength(60, ErrorMessage = "Nationality must not exceed 60 characters.")]
    public string Nationality { get; set; } = string.Empty;

    
    [Required(ErrorMessage = "Country of residence is required.")]
    [StringLength(60, ErrorMessage = "Country must not exceed 60 characters.")]
    public string CountryOfResidence { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one language is required.")]
    [MinLength(1, ErrorMessage = "Please select at least one language.")]
    public List<Language> Languages { get; set; } = new();
}