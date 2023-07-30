using Api.Dtos.Employee;
using Api.SalaryCostStrategies;
using Api.Utils;

namespace Api.Dtos.Paycheck;

public class CreatePaycheckDto
{
    public int PaycheckNumber { get; set; }
    public int EmployeeId { get; set; }
    public ICollection<CreatePaycheckItemDto> PaycheckItems { get; set; } = new List<CreatePaycheckItemDto>();
    public decimal PaycheckBaseSalary { get; set; }
    public decimal FinalAmount { get; set; }

    public CreatePaycheckDto() { }

    public CreatePaycheckDto(Models.Employee employeeModel,
        int index,
        IEnumerable<ISalaryCostStrategy> salaryCostStrategies)
    {
        PaycheckNumber = index;
        EmployeeId = employeeModel.Id;
        foreach (var salaryCostStrategy in salaryCostStrategies)
        {
            PaycheckItems.Add(new CreatePaycheckItemDto(employeeModel, salaryCostStrategy, index));
        }

        PaycheckBaseSalary = MoneyHelpers.CalculateEvenlyDistributedDivision(employeeModel.Salary,
            Constants.NumberOfBiweeklyBracketsPerYear, index);
        FinalAmount = PaycheckBaseSalary + PaycheckItems.Sum(item => item.Amount);
    }
}
