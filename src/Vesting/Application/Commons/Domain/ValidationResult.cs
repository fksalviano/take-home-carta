namespace Application.Commons.Domain;

public record ValidationResult
(
    bool IsValid, 
    string Error
);