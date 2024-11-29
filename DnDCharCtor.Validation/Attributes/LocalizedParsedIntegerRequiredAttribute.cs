using DnDCharCtor.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Validation.Attributes;

public class LocalizedParsedIntegerRequiredAttribute(string fieldNameResourceKey) : LocalizedRequiredAttribute(fieldNameResourceKey)
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valueStr = value?.ToString();
        if (string.IsNullOrWhiteSpace(valueStr)) return base.IsValid(valueStr, validationContext);

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
