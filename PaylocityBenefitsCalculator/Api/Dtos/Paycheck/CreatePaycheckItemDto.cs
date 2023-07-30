using Api.SalaryCostStrategies;

namespace Api.Dtos.Paycheck;

public class CreatePaycheckItemDto
{
    public ISalaryCostStrategy SalaryCostStrategy { get; set; }
    public decimal Amount { get; set; }

    public CreatePaycheckItemDto(Models.Employee employee, ISalaryCostStrategy salaryCostStrategy, int bracketIndex)
    {
        SalaryCostStrategy = salaryCostStrategy;
        Amount = salaryCostStrategy.CalculatePaycheckCost(employee, bracketIndex);
    }
}
