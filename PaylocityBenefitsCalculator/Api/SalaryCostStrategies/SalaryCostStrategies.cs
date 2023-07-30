using Api.Models;
using Api.Utils;

namespace Api.SalaryCostStrategies;

/// <summary>
/// Strategy pattern is ideal for this case because we can extend the logic easily in the future,
/// it's more readable and maintainable when each strategy for cost calculation is encapsulated.
/// (Better to move them to separate files.)
/// </summary>
public interface ISalaryCostStrategy
{
    decimal CalculateAnnualCost(Employee employee);

    decimal CalculatePaycheckCost(Employee employee, int bracketIndex) =>
        MoneyHelpers.CalculateEvenlyDistributedDivision(CalculateAnnualCost(employee),
            Constants.NumberOfBiweeklyBracketsPerYear, bracketIndex);

    /// <summary>
    /// Name makes the strategy human-readable in API response.
    /// </summary>
    string Name => GetType().Name;
}

public class BaseSalaryCostStrategy : ISalaryCostStrategy
{
    private const decimal FixedCost = 1000m;
    public decimal CalculateAnnualCost(Employee employee) => FixedCost;
}

public class SalaryCostPerDependentStrategy : ISalaryCostStrategy
{
    private const decimal DependentCost = 600m;

    /// <summary>
    /// This will throw an exception if the Dependents data is not legit.
    /// </summary>
    public decimal CalculateAnnualCost(Employee employee) =>
        employee.GetLegitimateDependentsOrThrow().Count * DependentCost;
}

public class HighPaidSalaryCostStrategy : ISalaryCostStrategy
{
    private const decimal HighPaidThreshold = 80000m;
    private const decimal HighPaidRatio = 0.02m;

    public decimal CalculateAnnualCost(Employee employee) =>
        employee.Salary * (employee.Salary > HighPaidThreshold ? HighPaidRatio : 0m);
}

public class OldDependentsSalaryCostStrategy : ISalaryCostStrategy
{
    private readonly IClock _clock;
    private const int OldAgeThreshold = 50;
    private const int OldAgeCost = 50;

    public OldDependentsSalaryCostStrategy(IClock clock)
    {
        _clock = clock;
    }

    /// <summary>
    /// This will throw an exception if the Dependents data is not legit.
    /// </summary>
    public decimal CalculateAnnualCost(Employee employee) =>
        OldAgeCost * employee.GetLegitimateDependentsOrThrow().Count(d => d.GetAge(_clock) > OldAgeThreshold);
}
