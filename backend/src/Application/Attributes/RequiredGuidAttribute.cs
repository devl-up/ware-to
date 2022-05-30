using System.ComponentModel.DataAnnotations;

namespace Application.Attributes;

public class RequiredGuidAttribute : RequiredAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is Guid guid)
        {
            return guid != Guid.Empty;
        }

        return base.IsValid(value);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is Guid guid)
        {
            return guid != Guid.Empty ? null : new ValidationResult(ErrorMessage, new[] {validationContext.MemberName}!);
        }

        return base.IsValid(value, validationContext);
    }
}