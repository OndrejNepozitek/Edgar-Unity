namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    public interface IConnection<out TRoom>
    {
        TRoom From { get; }

        TRoom To { get; }
    }
}