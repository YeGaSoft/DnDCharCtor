using DnDCharCtor.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Validation.Attributes;

public class LocalizedRequiredAttribute(string fieldName) : RequiredAttribute
{
    public string FieldName { get; set; } = fieldName;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var validationResult = base.IsValid(value, validationContext);
        if (validationResult == ValidationResult.Success) return validationResult;

        return new ValidationResult(string.Format(StringResources.Validation_RequiredField, FieldName));
    }
}
