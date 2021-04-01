namespace Edgar.Unity
{
    /// <summary>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="DungeonGeneratorPostProcessBase"/> for an actual implementation.
    /// The DungeonGeneratorPostProcessBase class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of DungeonGeneratorPostProcessBase will move to this file.
    /// </summary>
    #pragma warning disable 612, 618
    public class DungeonGeneratorPostProcessBaseGrid2D : DungeonGeneratorPostProcessBase
    #pragma warning restore 612, 618
    {
        /// <summary>
        /// Runs the post-processing logic with a given generated level and corresponding level description.
        /// </summary>
        public virtual void Run(GeneratedLevelGrid2D level, LevelDescriptionGrid2D levelDescription)
        {

        }
    }
}

