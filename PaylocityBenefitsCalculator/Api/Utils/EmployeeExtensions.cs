using Api.Models;

namespace Api.Utils;

public static class EmployeeExtensions
{
    //This method is the only place we write this logic
    public static bool HasIllegitimateDependents(this Employee employee) =>
        employee.Dependents.Count(d => d.Relationship is Relationship.Spouse or Relationship.DomesticPartner) > 1
        || employee.Dependents.Any(d => d.Relationship is Relationship.None);

    //A fancy way of reading Dependents and ensuring valid data. Normally we expect to get a nice validation error.
    // But this is useful because we make sure we never produce incorrect data.
    // Worst case scenario, we get a general error.
    public static ICollection<Dependent> GetLegitimateDependentsOrThrow(this Employee employee) =>
        employee.HasIllegitimateDependents() ? throw new Exception("Invalid Employee") : employee.Dependents;
}
