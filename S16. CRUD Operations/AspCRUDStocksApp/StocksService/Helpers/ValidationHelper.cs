using System.ComponentModel.DataAnnotations;

namespace StocksService.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Valida gli attributi del modello passato.
        /// Il parametro "all" se true effettua la verifica di tutte le proprietà del modello.
        /// Se false, valida solo quelle decorate con [Required]
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="all"></param>
        /// <exception cref="ArgumentException"></exception>
        internal static void ModelValidation(object obj, bool all)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Ritorna un valore booleano: true validazione passata / false errori di validazione
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, all);

            // Verifica della validazione
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
