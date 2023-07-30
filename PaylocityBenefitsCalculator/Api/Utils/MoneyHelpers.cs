namespace Api.Utils;

public static class MoneyHelpers
{
    public static decimal CalculateEvenlyDistributedDivision(decimal total, int numberOfBrackets, int bracketIndex)
    {
        var estimationPerBracket = new decimal((int)(100 * total) / numberOfBrackets) / 100m;
        var totalError = total - estimationPerBracket * numberOfBrackets;

        // numberOfErrorCents is between 0 and numberOfBrackets
        var numberOfErrorCents = (int)(totalError * 100);

        if (numberOfErrorCents == 0m)
            return estimationPerBracket;

        // borrow tells us every how many brackets we need to give back a cent
        var borrow = numberOfBrackets / numberOfErrorCents;
        // are we giving back a cent in this bracket?
        var isBorrow = bracketIndex % borrow == 0;

        return estimationPerBracket + (isBorrow ? 0.01m : 0m);
    }
}
