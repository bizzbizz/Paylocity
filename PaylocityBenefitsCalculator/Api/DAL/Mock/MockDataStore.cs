using Api.Models;

namespace Api.DAL.Mock;

/// <summary>
/// It's a static class but consumes no memory because Data is null at first.
/// Upon first access, everything will be set.
/// </summary>
public static class MockDataStore
{
    private static Dictionary<int, Employee>? _employees;
    private static Dictionary<int, Dependent>? _dependents;

    public static Dictionary<int, Employee> Employees
    {
        get
        {
            if(_employees == null)
                Reset();
            return _employees!;
        }
    }

    public static Dictionary<int, Dependent> Dependents
    {
        get
        {
            if(_dependents == null)
                Reset();
            return _dependents!;
        }
    }

    /// <summary>
    /// Excluded redundant information (such as Id) from the static code,
    /// because we can set them up programatically in the end.
    /// </summary>
    private static void Reset()
    {
        _employees = new Dictionary<int, Employee>()
        {
            {
                1, new()
                {
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                }
            },
            {
                2, new()
                {
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                    Dependents = new List<Dependent>
                    {
                        new()
                        {
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = Relationship.Spouse,
                            DateOfBirth = new DateTime(1998, 3, 3)
                        },
                        new()
                        {
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2020, 6, 23)
                        },
                        new()
                        {
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2021, 5, 18)
                        }
                    }
                }
            },
            {
                3, new()
                {
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                    Dependents = new List<Dependent>
                    {
                        new()
                        {
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = Relationship.DomesticPartner,
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
                }
            }
        };

        _dependents = new Dictionary<int, Dependent>();

        var dependentIdCounter = 1;
        foreach (var employee in _employees)
        {
            employee.Value.Id = employee.Key;
            foreach (var dependent in employee.Value.Dependents)
            {
                _dependents.Add(dependentIdCounter, dependent);

                dependent.Id = dependentIdCounter++;
                dependent.EmployeeId = employee.Key;
                dependent.Employee = employee.Value;
            }
        }
    }
}
