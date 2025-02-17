using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ApiStarPare.Tools
{
    public class PlacaVeiculoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string placa)
            {
                string patternAntigo = @"^[A-Z]{3}-\d{4}$";   // ABC-1234
                string patternMercosul = @"^[A-Z]{3}\d[A-Z]\d{2}$"; //ABC1D23

                if (Regex.IsMatch(placa, patternAntigo) || Regex.IsMatch(placa, patternMercosul))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("A placa deve estar no formato 'AAA-9999' ou 'AAA9A99'.");
        }
    }
}
