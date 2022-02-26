using System.Linq;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    ///     Base class for initializing room templates.
    /// </summary>
    public abstract class RoomTemplateInitializerBaseGrid2D : MonoBehaviour
    {
        public virtual void Initialize()
        {
            // Remove all children game objects
            foreach (var child in transform.Cast<Transform>().ToList())
            {
                PostProcessUtilsGrid2D.Destroy(child.gameObject);
            }

            // Add room template component
            if (gameObject.GetComponent<RoomTemplateSettingsGrid2D>() == null)
            {
                gameObject.AddComponent<RoomTemplateSettingsGrid2D>();
            }

            // Create tilemaps root
            var tilemapsRoot = new GameObject(GeneratorConstantsGrid2D.TilemapsRootName);
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
            if (gameObject.GetComponent<DoorsGrid2D>() == null)
            {
                gameObject.AddComponent<DoorsGrid2D>();
            }
        }
    }
}