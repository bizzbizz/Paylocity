namespace Api.Utils;

public static class MoneyHelpers
{
    /// <summary>
    /// We should remember the precision error and carry the discarded amount to the next month.
    /// This can only be done correctly if we store all the paychecks in database.
    /// But for now, I'm only producing estimated numbers.
    /// </summary>
    public static decimal DivideWithTwoDigitPrecision(decimal value1, decimal value2)
        => new decimal(100 * (int) value1 / (int) value2) / 100m;
}
