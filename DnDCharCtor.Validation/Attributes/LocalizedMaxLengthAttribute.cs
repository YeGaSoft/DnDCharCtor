using DnDCharCtor.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Validation.Attributes;

public class LocalizedMaxLengthAttribute(string fieldNameResourceKey, int maxLength) : MaxLengthAttribute(maxLength)
{
    public string FieldNameResourceKey { get; set; } = fieldNameResourceKey;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var validationResult = base.IsValid(value, validationContext);
        if (validationResult == ValidationResult.Success) return validationResult;

        string fieldName = StringResources.ResourceManager.GetString(FieldNameResourceKey, CultureInfo.CurrentCulture) ?? validationContext.MemberName ?? string.Empty;
        return new ValidationResult(string.Format(CultureInfo.CurrentCulture, StringResources.Validation_MaxLength, fieldName, Length), [validationContext.MemberName ?? string.Empty]);
    }
}
