namespace Edgar.Unity
{
    /// <summary>
    /// Controls how are room templates instantiated when in the Editor.
    /// If the game is in Play mode, the default Instantiate option is always used.
    /// </summary>
    public enum RoomTemplatePrefabModeGrid2D
    {
        /// <summary>
        /// Instantiate room templates using Object.Instantiate().
        /// </summary>
        Instantiate = 0,
        
        /// <summary>
        /// Instantiate room templates using PrefabUtility.InstantiatePrefab().
        /// This option keeps all prefab references intact.
        /// Works only in the Editor.
        /// </summary>
        InstantiatePrefab = 1,
        
        /// <summary>
        /// Instantiate room templates using PrefabUtility.InstantiatePrefab(),
        /// but unpack the root object using PrefabUtility.UnpackPrefabInstance().
        /// This option keeps all the prefab references except for the root object.
        /// Useful if you need to alter some room templates in the Editor after a level is generated.
        /// Works only in the Editor.
        /// </summary>
        InstantiatePrefabAndUnpackRoot = 2,
    }
}