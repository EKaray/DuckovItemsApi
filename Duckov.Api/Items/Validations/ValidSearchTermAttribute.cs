using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DuckovItemsApi.Items.Validations;

public partial class ValidSearchTermAttribute : ValidationAttribute
{
    private static readonly Regex _regex = ValidInput();
    private readonly int _maxLength;

    public ValidSearchTermAttribute(int maxLength = 50)
    {
        _maxLength = maxLength;
    }


    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is string term)
        {
            if (term.Length > _maxLength)
            {
                return new ValidationResult($"Search term cannot have more than {_maxLength} characters.");
            }

            if (!_regex.IsMatch(term))
            {
                return new ValidationResult($"{validationContext.DisplayName} can only contain letters, numbers, spaces, dots (.), dashes (-), and colons (:).");
            }

            return ValidationResult.Success;
        }


        return new ValidationResult("Search term is not a string.");
    }

    [GeneratedRegex(@"^[a-zA-Z0-9 .\-:]*$")]
    private static partial Regex ValidInput();
}