using System;
using System.Collections.Generic;
using Api.Models;
using Api.SalaryCostStrategies;
using Api.Utils;
using Xunit;

namespace ApiTests.UnitTests;

public class SalaryCostStrategiesTest
{
    private const decimal FixedCost = 1000m;
    private const decimal DependentCost = 600m;
    private const decimal OldAgeCost = 200m;

    public static TheoryData<Employee, decimal[]> Data =>
        new()
        {
            {
                new Employee()
                {
                    Salary = 10000m,
                },
                new[] {FixedCost, 0, 0, 0}
            },
            {
                new Employee()
                {
                    Salary = 10000m,
                    Dependents = new List<Dependent>()
                    {
                        new() {DateOfBirth = DateTime.Now, Relationship = Relationship.Child},
                        new() {DateOfBirth = DateTime.Now, Relationship = Relationship.Child}
                    }
                },
                new[] {FixedCost, 2 * DependentCost, 0, 0}
            },
            {
                new Employee()
                {
                    Salary = 100000m,
                },
                new[] {FixedCost, 0, 2000m, 0}
            },
            {
                new Employee()
                {
                    Salary = 100000m,
                    Dependents = new List<Dependent>()
                    {
                        new() {DateOfBirth = DateTime.Now.AddYears(-40), Relationship = Relationship.Child},
                        new() {DateOfBirth = DateTime.Now.AddYears(-60), Relationship = Relationship.Child },
                        new() {DateOfBirth = DateTime.Now.AddYears(-70), Relationship = Relationship.Spouse}
                    }
                },
                new[] {FixedCost, 3 * DependentCost, 2000m, 2 * OldAgeCost}
            }
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void BaseSalaryCostStrategyTheory(Employee employee, decimal[] expectedResult)
    {
        var actualResult = new BaseSalaryCostStrategy().CalculateAnnualCost(employee);
        Assert.Equal(expectedResult[0], actualResult);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void SalaryCostPerDependentStrategyTheory(Employee employee, decimal[] expectedResult)
    {
        var actualResult = new SalaryCostPerDependentStrategy().CalculateAnnualCost(employee);
        Assert.Equal(expectedResult[1], actualResult);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void HighPaidSalaryCostStrategyTheory(Employee employee, decimal[] expectedResult)
    {
        var actualResult = new HighPaidSalaryCostStrategy().CalculateAnnualCost(employee);
        Assert.Equal(expectedResult[2], actualResult);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void OldDependentsSalaryCostStrategyTheory(Employee employee, decimal[] expectedResult)
    {
        var actualResult = new OldDependentsSalaryCostStrategy(new LocalClock()).CalculateAnnualCost(employee);
        Assert.Equal(expectedResult[3], actualResult);
    }

    [Fact]
    public void OldDependentsSalaryCostStrategy_GivenInvalidDependents_ShouldThrow()
    {
        var employee = new Employee {Dependents = new List<Dependent> {new()}};
        Assert.Throws<Exception>(() =>
            new OldDependentsSalaryCostStrategy(new LocalClock()).CalculateAnnualCost(employee));
    }

    [Fact]
    public void SalaryCostPerDependentStrategy_GivenInvalidDependents_ShouldThrow()
    {
        var employee = new Employee {Dependents = new List<Dependent> {new()}};
        Assert.Throws<Exception>(() => new SalaryCostPerDependentStrategy().CalculateAnnualCost(employee));
    }
}
