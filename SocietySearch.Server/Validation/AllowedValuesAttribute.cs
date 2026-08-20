using System.ComponentModel.DataAnnotations;

namespace SocietySearch.Server.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedValuesAttribute(params string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue && _allowedValues.Contains(stringValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                $"{validationContext.DisplayName} must be one of: {string.Join(", ", _allowedValues)}.");
        }
    }
}
