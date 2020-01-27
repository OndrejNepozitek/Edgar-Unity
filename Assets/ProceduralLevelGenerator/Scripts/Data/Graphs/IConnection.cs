namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
    public interface IConnection<out TRoom>
    {
        TRoom From { get; }

        TRoom To { get; }
    }
}