using GeneralAlgorithms.DataStructures.Common;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Transformations
{
    /// <summary>
    ///     Currently not used.
    /// </summary>
    public class RoomTransformationsScript : MonoBehaviour
    {
        private readonly RoomTransformations roomTransformations = new RoomTransformations();
        public Transformation Transformation;

        public bool TransformDoors = true;

        public void Transform()
        {
            roomTransformations.Transform(gameObject, Transformation);

            // Transform doors if enabled
            if (TransformDoors)
            {
                var doors = gameObject.GetComponent<Doors.Doors>();

                if (doors != null)
                {
                    doors.Transform(Transformation);
                }
            }
        }
    }
}