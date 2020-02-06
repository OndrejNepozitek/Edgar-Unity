using System.Collections;
using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private GameObject canvas;

        public int CurrentLevel;
        private GungeonRoomType currentRoomType;

        private DungeonGeneratorRunner dungeonGenerator;
        private GameObject levelImage;
        private Text levelInfoText;
        public GungeonLevel[] Levels;
        public float LevelStartDelay = 2f;
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