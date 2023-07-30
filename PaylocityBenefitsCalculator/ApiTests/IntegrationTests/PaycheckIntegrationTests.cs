using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Paycheck;
using Api.SalaryCostStrategies;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaycheckIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAPaycheck_ShouldReturnCorrectPaycheck()
    {
        var response = await HttpClient.GetAsync("/api/v1/paychecks/2/0");
        var dependent = new CreatePaycheckDto
        {
            PaycheckNumber = 0,
            FinalAmount = 3731.27m,
            PaycheckBaseSalary = 3552.51m,
            EmployeeId = 2,
            PaycheckItems = new List<CreatePaycheckItemDto>
            {
                new() {SalaryCostStrategyName = nameof(BaseSalaryCostStrategy), Amount = 38.47m},
                new() {SalaryCostStrategyName = nameof(SalaryCostPerDependentStrategy), Amount = 69.24m},
                new() {SalaryCostStrategyName = nameof(HighPaidSalaryCostStrategy), Amount = 71.05m},
                new() {SalaryCostStrategyName = nameof(OldDependentsSalaryCostStrategy), Amount = 0},
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    public async Task WhenAskedForPaycheckForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/paychecks/{int.MinValue}/0");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    //todo cover more cases for invalid index, invalid dependents or failed validations
}
