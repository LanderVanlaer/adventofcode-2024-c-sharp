namespace _07_1;

public record Equation(long Result, long[] Numbers)
{
    public bool IsValidEquation()
    {
        return IsValidEquationSum(Numbers[0], 1) || IsValidEquationProduct(Numbers[0], 1);
    }

    private bool IsValidEquationSum(long result, int i)
    {
        result += Numbers[i];

        if (i == Numbers.Length - 1) return result == Result;

        return IsValidEquationSum(result, i + 1) || IsValidEquationProduct(result, i + 1);
    }

    private bool IsValidEquationProduct(long result, int i)
    {
        result *= Numbers[i];

        if (i == Numbers.Length - 1) return result == Result;

        return IsValidEquationSum(result, i + 1) || IsValidEquationProduct(result, i + 1);
    }
}