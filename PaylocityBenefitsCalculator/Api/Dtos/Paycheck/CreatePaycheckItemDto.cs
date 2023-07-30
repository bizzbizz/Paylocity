using Api.SalaryCostStrategies;

namespace Api.Dtos.Paycheck;

public class CreatePaycheckItemDto
{
    public string SalaryCostStrategyName { get; set; }
    public decimal Amount { get; set; }

    public CreatePaycheckItemDto() { }

    public CreatePaycheckItemDto(Models.Employee employee, ISalaryCostStrategy salaryCostStrategy, int bracketIndex)
    {
        SalaryCostStrategyName = salaryCostStrategy.Name;
        Amount = salaryCostStrategy.CalculatePaycheckCost(employee, bracketIndex);
    }
}
