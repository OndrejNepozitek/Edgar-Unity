using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    public abstract class GameManagerBase<TGameManager> : MonoBehaviour 
        where TGameManager : class
    {
        public static TGameManager Instance;

        public GameObject Canvas;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this as TGameManager;
            }
            else if (!ReferenceEquals(Instance, this))
            {
                Destroy(gameObject);
                return;
            }

            if (Canvas != null)
            {
                Canvas.SetActive(true);
            }

            DontDestroyOnLoad(gameObject);
        }

        protected virtual void SingletonAwake()
        {

        }

        public abstract void LoadNextLevel();

        protected virtual void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            LoadNextLevel();
        }

        protected void SetLevelInfo(string text)
        {
            var canvas = GetCanvas();
            var levelInfo = canvas.transform.Find("LevelInfo")?.gameObject.GetComponent<Text>();

            if (levelInfo != null)
            {
                levelInfo.text = text;
            }
        }

        protected void ShowLoadingScreen(string primaryText, string secondaryText)
        {
            var canvas = GetCanvas();
            var loadingImage = canvas.transform.Find("LoadingImage")?.gameObject;
            var primaryTextComponent = loadingImage?.transform.Find("PrimaryText")?.gameObject.GetComponent<Text>();
            var secondaryTextComponent = loadingImage?.transform.Find("SecondaryText")?.gameObject.GetComponent<Text>();

            if (loadingImage != null)
            {
                loadingImage.SetActive(true);
            }

            if (primaryTextComponent != null)
            {
                primaryTextComponent.text = primaryText;
            }

            if (secondaryTextComponent != null)
            {
                secondaryTextComponent.text = secondaryText;
            }
        }

        protected GameObject GetCanvas()
        {
            var canvas = Canvas ?? GameObject.Find("Canvas");

            if (canvas == null)
            {
                throw new InvalidOperationException($"Canvas was not found. Please set the {nameof(Canvas)} variable of the GameManager");
            }

            return canvas;
        }

        protected void HideLoadingScreen()
        {
            var canvas = GetCanvas();
            var loadingImage = canvas.transform.Find("LoadingImage")?.gameObject;

            if (loadingImage != null)
            {
                loadingImage.SetActive(false);
            }
        }

        public virtual void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        public virtual void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }
}