namespace Edgar.Unity.Diagnostics
{
    public interface IDiagnosticResult
    {
        string Name { get; }

        string Summary { get; }

        bool IsPotentialProblem { get; }
    }
}