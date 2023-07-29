using Api.SalaryCostStrategies;

namespace Api.Dtos.Paycheck;

public class CreatePaycheckItemDto
{
    public ISalaryCostStrategy SalaryCostStrategy { get; set; }
    public decimal Amount { get; set; }

    public CreatePaycheckItemDto(Models.Employee employee, ISalaryCostStrategy salaryCostStrategy)
    {
        SalaryCostStrategy = salaryCostStrategy;
        Amount = salaryCostStrategy.CalculatePaycheckCost(employee);
    }
}
