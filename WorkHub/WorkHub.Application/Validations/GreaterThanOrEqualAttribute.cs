using System.ComponentModel.DataAnnotations;
using WorkHub.Application.Dtos.Space;

namespace WorkHub.Application.Validations
{
    public class GreaterThanOrEqualAttribute:ValidationAttribute
    {
        private string _comparisonProperty;
        public GreaterThanOrEqualAttribute(string comparisonProperty,string errorMessage)
        {
            _comparisonProperty = comparisonProperty;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage); 
            }

            int maxValue ;
            if(value is int max)
            {
                maxValue = max;
            }
            else
            {
                return new ValidationResult($"The value of the property '{validationContext.MemberName}' is not of type int.");
            }


            var minValue = validationContext.ObjectInstance.GetType().GetProperty(_comparisonProperty);
            if(minValue == null)
            {
                return new ValidationResult($"Property '{_comparisonProperty}' not found.");
            }
            if(minValue.GetValue(validationContext.ObjectInstance) is int min)
            {
                if (maxValue >= min)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessage);
            }
           
            return new ValidationResult($"Property '{_comparisonProperty}' is not of type int.");
        }
    }
}
