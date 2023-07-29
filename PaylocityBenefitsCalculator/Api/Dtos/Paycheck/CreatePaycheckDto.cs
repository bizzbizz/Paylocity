using Api.Dtos.Employee;
using Api.SalaryCostStrategies;
using Api.Utils;

namespace Api.Dtos.Paycheck;

public class CreatePaycheckDto
{
    public int PaycheckNumber { get; set; }
    public GetEmployeeDto Employee { get; set; }
    public ICollection<CreatePaycheckItemDto> PaycheckItems { get; set; } = new List<CreatePaycheckItemDto>();
    public decimal PaycheckBaseSalary { get; set; }
    public decimal FinalAmount { get; set; }

    public CreatePaycheckDto(Models.Employee employeeModel,
        int index,
        IEnumerable<ISalaryCostStrategy> salaryCostStrategies)
    {
        PaycheckNumber = index;
        Employee = new GetEmployeeDto(employeeModel);
        foreach (var salaryCostStrategy in salaryCostStrategies)
        {
            PaycheckItems.Add(new CreatePaycheckItemDto(employeeModel, salaryCostStrategy));
        }
        PaycheckBaseSalary = MoneyHelpers.DivideWithTwoDigitPrecision(employeeModel.Salary, 26);
        FinalAmount = PaycheckBaseSalary + PaycheckItems.Sum(item => item.Amount);
    }
}
