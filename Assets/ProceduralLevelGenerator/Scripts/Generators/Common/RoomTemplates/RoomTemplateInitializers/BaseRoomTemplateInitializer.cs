using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.RoomTemplateInitializers
{
    /// <summary>
    ///     Base class for initializing room templates.
    /// </summary>
    public abstract class BaseRoomTemplateInitializer : MonoBehaviour
    {
        public virtual void Initialize()
        {
            // Remove all children game objects
            foreach (var child in transform.Cast<Transform>().ToList())
            {
                PostProcessUtils.Destroy(child.gameObject);
            }

            // Add room template component
            if (gameObject.GetComponent<RoomTemplate>() == null)
            {
                gameObject.AddComponent<RoomTemplate>();
            }

            // Create tilemaps root
            var tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
            tilemapsRoot.transform.parent = gameObject.transform;

            // Init tilemaps
            InitializeTilemaps(tilemapsRoot);

            // Fix positions
            tilemapsRoot.transform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;

            // Add doors
            InitializeDoors();
        }

        protected virtual void InitializeTilemaps(GameObject tilemapsRoot)
        {

        }

        protected virtual void InitializeDoors()
        {
            // Add Doors component
            if (gameObject.GetComponent<Doors.Doors>() == null)
            {
                gameObject.AddComponent<Doors.Doors>();
            }
        }
    }
}