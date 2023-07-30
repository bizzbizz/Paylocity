using Api.Utils;
using Xunit;

namespace ApiTests.UnitTests;

public class MoneyHelperTest
{
    public static TheoryData<decimal, int, int, decimal> Data =>
        new()
        {
            {1m, 2, 0, 0.5m},
            {1m, 2, 1, 0.5m},
            {1m, 3, 0, 0.34m},
            {1m, 3, 1, 0.33m},
            {1m, 3, 2, 0.33m}
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void CalculateEvenlyDistributedDivisionTheory(
        decimal total, int numberOfBrackets, int bracketIndex, decimal expectedResult)
    {
        var actualResult = MoneyHelpers.CalculateEvenlyDistributedDivision(total, numberOfBrackets, bracketIndex);
        Assert.Equal(actualResult, expectedResult);
    }
    
}
