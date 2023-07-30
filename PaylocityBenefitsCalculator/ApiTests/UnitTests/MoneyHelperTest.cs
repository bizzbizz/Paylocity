using Api.Utils;
using Xunit;

namespace ApiTests.UnitTests;

public class MoneyHelperTest
{
    public static TheoryData<decimal, int, int, decimal> Data =>
        new()
        {
            // When we have no brackets to compensate
            {1m, 2, 0, 0.5m},
            {1m, 2, 1, 0.5m},
            // When we have one bracket to compensate
            {1m, 3, 0, 0.33m},
            {1m, 3, 1, 0.33m},
            {1m, 3, 2, 0.34m},
            // When we have more than one bracket to compensate, and last bracket has to compensate
            {1.02m, 4, 0, 0.25m},
            {1.02m, 4, 1, 0.26m},
            {1.02m, 4, 0, 0.25m},
            {1.02m, 4, 1, 0.26m},
            // When we have more than one bracket to compensate, and last bracket does not compensate
            {1.02m, 5, 0, 0.20m},
            {1.02m, 5, 1, 0.21m},
            {1.02m, 5, 2, 0.20m},
            {1.02m, 5, 3, 0.21m},
            {1.02m, 5, 4, 0.20m},
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void CalculateEvenlyDistributedDivisionTheory(
        decimal total, int numberOfBrackets, int bracketIndex, decimal expectedResult)
    {
        var actualResult = MoneyHelpers.CalculateEvenlyDistributedDivision(total, numberOfBrackets, bracketIndex);
        Assert.Equal(expectedResult, actualResult);
    }
    
}
