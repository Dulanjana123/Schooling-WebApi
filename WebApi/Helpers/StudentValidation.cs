using System.ComponentModel.DataAnnotations;

public static class StudentValidation
{
    public static ValidationResult ValidateDateOfBirth(DateOnly dateOfBirth, ValidationContext context)
    {
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today))
        {
            return new ValidationResult("Date of Birth cannot be in the future.");
        }
        return ValidationResult.Success!;
    }
}

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(ErrorMessage ?? $"File type not allowed. Allowed types: {string.Join(", ", _extensions)}");
            }
        }
        return ValidationResult.Success!;
    }
}

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxSize;

    public MaxFileSizeAttribute(int maxSize)
    {
        _maxSize = maxSize;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > _maxSize)
            {
                return new ValidationResult(ErrorMessage ?? $"File size exceeds the limit of {_maxSize / 1024 / 1024}MB.");
            }
        }
        return ValidationResult.Success!;
    }
}
