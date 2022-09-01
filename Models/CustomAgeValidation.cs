using System.ComponentModel.DataAnnotations;
namespace BankManagementDotnetApi.Models;
public class CustomAgeValidation : ValidationAttribute
{  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) // The name of the context of the field which we are validating
        {
            DateTime _toDate = Convert.ToDateTime(value);
            var age=DateTime.Today.Year-_toDate.Year;
            if(age>=10)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    

}