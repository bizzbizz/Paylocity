using System.Collections.Generic;
using System.Linq;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.SalaryCostStrategies;
using Api.Utils;
using Xunit;

namespace ApiTests.UnitTests;

/// <summary>
/// ideally this would test a dependency and not a dto.
/// But for the sake of simplicity we put the logic in ctor of CreatePaycheckDto, so now we're testing the ctor.
/// </summary>
public class CreatePaycheckDtoTest
{
    private static readonly IEnumerable<ISalaryCostStrategy> Strategies = new[]
    {
        new TestBaseSalaryCostStrategy(),
        new TestBaseSalaryCostStrategy()
    };

    [Fact]
    public void CreatePaycheckDtoConstructor()
    {
        const decimal salaryPerBracket = 100m;
        var employee = new Employee
        {
            Id = 1,
            Salary = salaryPerBracket * Constants.NumberOfBiweeklyBracketsPerYear
        };

        var dto = new CreatePaycheckDto(employee, 1, Strategies);

        Assert.Equal(1, dto.PaycheckNumber);
        Assert.Equal(1, dto.EmployeeId);
        Assert.Equal(salaryPerBracket, dto.PaycheckBaseSalary);
        Assert.Equal(salaryPerBracket + TestBaseSalaryCostStrategy.FixedAmountPerBracket * Strategies.Count(),
            dto.FinalAmount);
    }

    class TestBaseSalaryCostStrategy : ISalaryCostStrategy
    {
        public const decimal FixedAmountPerBracket = 10m;

        public decimal CalculateAnnualCost(Employee employee) =>
            FixedAmountPerBracket * Constants.NumberOfBiweeklyBracketsPerYear;
    }
}
