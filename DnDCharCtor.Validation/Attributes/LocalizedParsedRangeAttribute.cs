using DnDCharCtor.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Validation.Attributes;

public class LocalizedParsedRangeAttribute(string fieldNameResourceKey, int minimum, int maximum) : LocalizedRangeAttribute(fieldNameResourceKey, minimum, maximum)
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valueStr = value?.ToString();
        // When the field is empty, there is nothing we can parse.
        // This means this field is not required and thus we must not show an error message whgen there is nothing to parse.
        if (string.IsNullOrWhiteSpace(valueStr)) return ValidationResult.Success;

        if (int.TryParse(valueStr, out int parsedValue))
        {
            return base.IsValid(parsedValue, validationContext);
        }
        else
        {
            string fieldName = StringResources.ResourceManager.GetString(FieldNameResourceKey) ?? validationContext.MemberName ?? string.Empty;
            return new ValidationResult(string.Format(StringResources.Validation_RequiredParsedInteger, fieldName), [validationContext.MemberName ?? string.Empty]);
        }
    }
}
