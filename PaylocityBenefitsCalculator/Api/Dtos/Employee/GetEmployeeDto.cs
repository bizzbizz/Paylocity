using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();

    public GetEmployeeDto() { }

    /// <summary>
    /// Adding this ctor makes it easier to convert from model to dto. We can also chain-convert the child objects too.
    /// There are more sophisticated patterns, but better to respect KISS.
    /// </summary>
    /// <param name="model"></param>
    public GetEmployeeDto(Models.Employee model)
    {
        Id = model.Id;
        FirstName = model.FirstName;
        LastName = model.LastName;
        Salary = model.Salary;
        DateOfBirth = model.DateOfBirth;
        Dependents = model.Dependents.Select(dependentModel => new GetDependentDto(dependentModel)).ToArray();
    }
}
