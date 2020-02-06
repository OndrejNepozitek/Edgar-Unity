using System;
using System.Collections;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts
{
    public class DeadCellsGameManager : MonoBehaviour
    {
        public static DeadCellsGameManager Instance;
        public float LevelStartDelay = 2f;
        public DeadCellsLevelType LevelType;

        private GameObject canvas;
        private DungeonGeneratorPipeline dungeonGeneratorPipeline;
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
                return;
            }

            DontDestroyOnLoad(gameObject);
            InitLevel();
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.T))
            {
                SwitchLevel();
            }

            if (Input.GetKey(KeyCode.G))
            {
                InitLevel();
            }
        }

        public void SwitchLevel()
        {
            switch (LevelType)
            {
                case DeadCellsLevelType.Rooftop:
                    LevelType = DeadCellsLevelType.Underground;
                    SceneManager.LoadScene("DeadCellsUnderground");
                    break;

                case DeadCellsLevelType.Underground:
                    LevelType = DeadCellsLevelType.Rooftop;
                    SceneManager.LoadScene("DeadCellsRamparts");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            InitLevel();
        }

        public void InitLevel()
        {
            dungeonGeneratorPipeline = GameObject.Find("Dungeon Generator").GetComponent<DungeonGeneratorPipeline>();
            canvas = GameObject.Find("Canvas");
            levelImage = canvas.transform.Find("LevelImage").gameObject;
            levelText = levelImage.transform.Find("LevelText").gameObject.GetComponent<Text>();
            levelInfoText = canvas.transform.Find("LevelInfo").gameObject.GetComponent<Text>();
            levelText.text = $"{LevelType} level";

            levelImage.SetActive(true);
            StartCoroutine(GeneratorCoroutine());
        }

        private IEnumerator GeneratorCoroutine()
        {
            yield return null;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            dungeonGeneratorPipeline.Generate();

            levelInfoText.text = $"Current level: {LevelType}\nGenerated in {stopwatch.ElapsedMilliseconds/1000d:F}s";

            yield return new WaitForSeconds(1);
            levelImage.SetActive(false);
        }

        public void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        public void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }
}