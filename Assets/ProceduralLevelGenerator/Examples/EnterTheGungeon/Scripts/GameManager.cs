using System.Collections;
using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public int CurrentLevel;
        public GungeonLevel[] Levels;
        public float LevelStartDelay = 2f;
        public FogOfWar FogOfWar;
        
        private GungeonRoomType currentRoomType;

        private GameObject canvas;
        private DungeonGeneratorRunner dungeonGenerator;
        private GameObject levelImage;
        private Text levelInfoText;
        private Text levelText;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            InitLevel();
        }

        public void RevealRoom(GungeonRoom room, RoomInstance roomInstance)
        {
            FogOfWar.VisionGrid.AddPolygon(GetPolygon(roomInstance), (Vector2Int) roomInstance.Position, 1);

            foreach (var doorInstance in roomInstance.Doors)
            {
                var neighbor = doorInstance.ConnectedRoom;

                if (neighbor.IsCorridor)
                {
                    FogOfWar.VisionGrid.AddPolygon(GetPolygon(neighbor), (Vector2Int) neighbor.Position, 1);
                }
            }
        }

        private Polygon2D GetPolygon(RoomInstance roomInstance)
        {
            var tilemaps = RoomTemplateUtils.GetTilemaps(roomInstance.RoomTemplateInstance);
            var outlineTilemaps = RoomTemplateUtils.GetTilemapsForOutline(tilemaps);
            var usedTiles = RoomTemplatesLoaderTest.GetUsedTiles(outlineTilemaps);
            var newPolygon = RoomTemplatesLoader.GetPolygonFromTiles(usedTiles);
            return new Polygon2D(newPolygon);
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            InitLevel();
        }

        private void InitLevel()
        {
            dungeonGenerator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGeneratorRunner>();
            canvas = GameObject.Find("Canvas");
            levelImage = canvas.transform.Find("LevelImage").gameObject;
            levelText = levelImage.transform.Find("LevelText").gameObject.GetComponent<Text>();
            levelInfoText = canvas.transform.Find("LevelInfo").gameObject.GetComponent<Text>();

            levelText.text = $"Level {CurrentLevel}";
            levelImage.SetActive(true);

            StartCoroutine(GeneratorCoroutine());
        }

        private IEnumerator GeneratorCoroutine()
        {
            yield return null;
            dungeonGenerator.Generate();
            yield return new WaitForSeconds(2);
            levelImage.SetActive(false);
        }

        private void RefreshLevelInfo()
        {
            levelInfoText.text = $"Room: {currentRoomType}";
        }

        public void SetCurrentRoomType(GungeonRoomType type)
        {
            currentRoomType = type;
            RefreshLevelInfo();
        }

        public void Enable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        public void Disable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }
}