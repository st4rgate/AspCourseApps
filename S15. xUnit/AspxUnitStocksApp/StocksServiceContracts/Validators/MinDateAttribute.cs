using System.ComponentModel.DataAnnotations;

namespace StocksServiceContracts.Validators
{
    public class MinDateAttribute : ValidationAttribute
    {
        // Empty Constructor
        public MinDateAttribute()
        {
        }
        // Costruttore che riceve in modo dinamico il valore minimo per l'anno
        private string _dateMin { get; set; } = "2000-01-01";
        private string DefaultErrorMessage { get; set; } = "Minimum date allowed is {0}";
        public MinDateAttribute(string dateMin)
        {
            _dateMin = dateMin;
        }
        // Override IsValid
        // value is the min date and validationContex contains validation details and reference to model object
        // the date must be in the format "yyyy-MM-dd"
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateToVerify = (DateTime)value;
                //if (dateToVerify > dateMin) return new ValidationResult("Minimum date allowed is {_dateMin}");
                // custom message with ErrorMessage= and parameter {0} that contains "_dateMin" value
                // or default message if custom is null (ErrorMessage?? DefaultErrorMessage)
                if (dateToVerify > Convert.ToDateTime(_dateMin)) return new ValidationResult(String.Format(ErrorMessage ?? DefaultErrorMessage, _dateMin));

                else return ValidationResult.Success;
            }
            return null;
        }
    }
}
