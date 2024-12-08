namespace _07_2;

public record Equation(long Result, long[] Numbers)
{
    public bool IsValidEquation()
    {
        return IsValidEquation(Numbers[0], 1);
    }

    private bool IsValidEquation(long result, int i)
    {
        if (i >= Numbers.Length) return result == Result;

        return IsValidEquationSum(result, i)
               || IsValidEquationProduct(result, i)
               || IsValidEquationConcatenation(result, i);
    }

    private bool IsValidEquationSum(long result, int i)
    {
        return IsValidEquation(result + Numbers[i], i + 1);
    }

    private bool IsValidEquationProduct(long result, int i)
    {
        return IsValidEquation(result * Numbers[i], i + 1);
    }

    private bool IsValidEquationConcatenation(long result, int i)
    {
        return IsValidEquation(result * (long)Math.Pow(10, Math.Floor(Math.Log10(Numbers[i])) + 1) + Numbers[i], i + 1);
    }
}