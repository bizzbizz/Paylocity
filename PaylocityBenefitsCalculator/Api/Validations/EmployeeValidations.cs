using Api.Models;
using Api.Utils;

namespace Api.Validations;

public interface IEmployeeValidations
{
    bool Validate(Employee employee, out string? error);
}

public class EmployeeValidations : IEmployeeValidations
{
    /// <summary>
    /// I don't like ref because of its limiting nature, but at the moment it does the work.
    /// </summary>
    public bool Validate(Employee employee, out string? error)
    {
        error = null;
        return ValidateDependents(employee, ref error);
    }

    /// <summary>
    /// put each validation in a separate method and chain them together in Validate method
    /// </summary>
    private static bool ValidateDependents(Employee employee, ref string? error)
    {
        if (!employee.HasIllegitimateDependents()) return true;

        error = $"Expected employee to have maximum of 1 {Relationship.Spouse} or {Relationship.DomesticPartner}.";
        return false;
    }
}
