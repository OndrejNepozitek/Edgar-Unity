namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces
{
    public interface IBenchmarkInfoPayload
    {
        GeneratedLevel GeneratedLevel { get; set; }

        int Iterations { get; set; }

        double TimeTotal { get; set; }
    }
}