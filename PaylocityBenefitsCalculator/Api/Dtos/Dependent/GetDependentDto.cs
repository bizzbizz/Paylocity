using Api.Models;

namespace Api.Dtos.Dependent;

public class GetDependentDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }

    public GetDependentDto() { }

    public GetDependentDto(Models.Dependent model)
    {
        Id = model.Id;
        FirstName = model.FirstName;
        LastName = model.LastName;
        DateOfBirth = model.DateOfBirth;
        Relationship = model.Relationship;
    }
}
