using Api.Models;

namespace Api.Utils;

public static class DependentExtensions
{
    public static int GetAge(this Dependent dependent, IClock clock)
    {
        var today = clock.Now.Date;
        var age = today.Year - dependent.DateOfBirth.Year;
        return dependent.DateOfBirth.Date > today.AddYears(-age) ? age - 1 : age;
    }
}