using System;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for custom post-processing logic.
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="DungeonGeneratorPostProcessBase"/> for an actual implementation.
    /// The DungeonGeneratorPostProcessBase class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of DungeonGeneratorPostProcessBase will move to this file.
    /// </remarks>
    #pragma warning disable 612, 618
    public class DungeonGeneratorPostProcessingGrid2D : DungeonGeneratorPostProcessBase, IDungeonGeneratorPostProcessing<DungeonGeneratorLevelGrid2D>
    #pragma warning restore 612, 618
    {
        /// <inheritdoc />
        public virtual void Run(DungeonGeneratorLevelGrid2D level)
        {

        }
    }
}

