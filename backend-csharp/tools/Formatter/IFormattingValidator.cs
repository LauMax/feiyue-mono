namespace Feiyue.Formatter;

internal interface IFormattingValidator
{
    Task<FormattingValidatorResult> ValidateAsync(CancellationToken cancellationToken);
}

internal sealed class FormattingValidatorResult
{
    public bool Failed { get; set; }
    public string? FailureMessage { get; set; }
}