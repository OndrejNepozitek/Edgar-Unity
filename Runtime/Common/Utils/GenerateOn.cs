namespace Edgar.Unity
{
    /// <summary>
    /// Controls when a generator gets called.
    /// </summary>
    public enum GenerateOn
    {
        /// <summary>
        /// Generator does not get called automatically, you must call it via code.
        /// </summary>
        Manually,
        
        /// <summary>
        /// Generator is called on Awake.
        /// </summary>
        Awake,
        
        /// <summary>
        /// Generator is called on Start.
        /// </summary>
        Start
    }
}