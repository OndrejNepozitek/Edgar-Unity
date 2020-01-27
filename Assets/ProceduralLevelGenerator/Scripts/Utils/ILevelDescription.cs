using GeneralAlgorithms.DataStructures.Graphs;
using MapGeneration.Interfaces.Core.MapDescriptions;

namespace Assets.ProceduralLevelGenerator.Scripts.Utils
{
    public interface ILevelDescription<TRoom, TConnection>
    {
        IMapDescription<TRoom> GetMapDescription();

        IGraph<TRoom> GetGraph();

        IGraph<TRoom> GetGraphWithoutCorridors();
    }
}