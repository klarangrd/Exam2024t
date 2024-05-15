using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
    public class RequiredDropdownSelectionAttribute : ValidationAttribute
    {
        private readonly string _defaultOption;

        public RequiredDropdownSelectionAttribute(string defaultOption)
        {
            _defaultOption = defaultOption;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) || value.ToString() == _defaultOption)
            {
                return new ValidationResult(ErrorMessage ?? "Dette felt er påkrævet.");
            }

            return ValidationResult.Success;
        }
    }
}
