using System.Collections.Generic;
using Api.Models;
using Api.Utils;
using Xunit;

namespace ApiTests.UnitTests;

public class EmployeeExtensionTest
{
    public static TheoryData<Employee, bool> Data =>
        new()
        {
            {new Employee() {Dependents = new List<Dependent>()}, false},
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Child}
                    }
                },
                false
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Spouse}
                    }
                },
                false
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.DomesticPartner}
                    }
                },
                false
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Spouse},
                        new() {Relationship = Relationship.DomesticPartner}
                    }
                },
                true
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Child},
                        new() {Relationship = Relationship.Spouse},
                        new() {Relationship = Relationship.Spouse}
                    }
                },
                true
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Spouse},
                        new() {Relationship = Relationship.Spouse},
                        new() {Relationship = Relationship.Spouse}
                    }
                },
                true
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.Spouse},
                        new() {Relationship = Relationship.DomesticPartner},
                    }
                },
                true
            },
            {
                new Employee()
                {
                    Dependents = new List<Dependent>
                    {
                        new() {Relationship = Relationship.None},
                    }
                },
                true
            },
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void HasIllegitimateDependentsTheory(Employee employee, bool expectedResult)
    {
        var actualResult = employee.HasIllegitimateDependents();
        Assert.Equal(expectedResult, actualResult);
    }
}
