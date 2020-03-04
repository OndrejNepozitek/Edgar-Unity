using System.Collections;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common
{
    public interface ILevelGenerator
    {
        void Generate();

        IEnumerator GenerateCoroutine();
    }
}