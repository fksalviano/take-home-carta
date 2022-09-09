using System.ComponentModel;

namespace Application.UseCases.GetVested.Domain;

public enum VestingType
{
    [Description("VEST")]
    VEST,

    [Description("CANCEL")]
    CANCEL
}