namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public interface IPostProcessTask<TCallback>
    {
        void RegisterCallbacks(PriorityCallbacks<TCallback> callbacks);
    }
}