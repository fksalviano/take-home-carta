namespace Application.Commons.Domain;

public record InputArguments
(
    string FileName, 
    DateTime TargetDate, 
    int Digits = 0
);
