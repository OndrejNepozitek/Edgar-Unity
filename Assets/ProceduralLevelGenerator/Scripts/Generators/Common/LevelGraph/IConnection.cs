namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph
{
    public interface IConnection<out TRoom>
    {
        TRoom From { get; }

        TRoom To { get; }
    }
}