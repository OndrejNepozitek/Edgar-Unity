namespace Edgar.Unity.Examples
{
    /// <summary>
    /// An interface for all objects that the player can interact with.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Called once the interaction begins (i.e. this object gets the focus of the player).
        /// In our simplified scenario, this happens when the player enters the collider of this object.
        /// </summary>
        void BeginInteract();

        /// <summary>
        /// Called during each frame when the this object is the focus of the player.
        /// </summary>
        void Interact();

        /// <summary>
        /// Called once the interaction ends (i.e. this object loses the focus of the player).
        /// In our simplified scenario, this happens when the player exits the collider of this object,
        /// when IsInteractionAllowed returns false or when a different object gets into focus.
        /// </summary>
        void EndInteract();

        /// <summary>
        /// Used to check whether the object wants to interact with the player.
        /// </summary>
        /// <returns></returns>
        bool IsInteractionAllowed();
    }
}