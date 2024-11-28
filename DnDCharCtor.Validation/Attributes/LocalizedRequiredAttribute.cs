using DnDCharCtor.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Validation.Attributes;

public class LocalizedRequiredAttribute(string fieldNameResourceKey) : RequiredAttribute
{
    public string FieldNameResourceKey { get; set; } = fieldNameResourceKey;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var validationResult = base.IsValid(value, validationContext);
        if (validationResult == ValidationResult.Success) return validationResult;

        string fieldName = StringResources.ResourceManager.GetString(FieldNameResourceKey) ?? validationContext.MemberName ?? string.Empty;
        return new ValidationResult(string.Format(StringResources.Validation_RequiredField, fieldName), [validationContext.MemberName ?? string.Empty]);
    }
}
