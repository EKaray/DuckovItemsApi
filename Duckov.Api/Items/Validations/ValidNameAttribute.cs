using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Duckov.Api.Items.Validations;

public partial class ValidNameAttribute : ValidationAttribute
{
    private static readonly Regex _regex = ValidInput();
    private readonly int _maxLength;

    public ValidNameAttribute(int maxLength = 50)
    {
        _maxLength = maxLength;
    }

    /// <summary>
    /// Search term. Only letters, numbers, spaces, dots (.), dashes (-), and colons (:). Max length specified.
    /// </summary>
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