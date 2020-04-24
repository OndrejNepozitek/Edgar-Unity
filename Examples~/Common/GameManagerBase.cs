using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProceduralLevelGenerator.Unity.Examples.Common
{
    /// <summary>
    /// Game manager base class.
    /// </summary>
    /// <typeparam name="TGameManager">Actual type of the game manager</typeparam>
    public abstract class GameManagerBase<TGameManager> : MonoBehaviour 
        where TGameManager : class
    {
        /// <summary>
        /// Singleton instance of the game manager.
        /// </summary>
        public static TGameManager Instance;

        /// <summary>
        /// UI canvas.
        /// </summary>
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

            SingletonAwake();
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void SingletonAwake()
        {

        }

        /// <summary>
        /// Load next level.
        /// </summary>
        public abstract void LoadNextLevel();

        /// <summary>
        /// Load next level on scene finished loading.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        protected virtual void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            LoadNextLevel();
        }

        /// <summary>
        /// Display information about the level like time to generate the level, etc.
        /// </summary>
        /// <param name="text"></param>
        protected void SetLevelInfo(string text)
        {
            var canvas = GetCanvas();
            var levelInfo = canvas.transform.Find("LevelInfo")?.gameObject.GetComponent<Text>();

            if (levelInfo != null)
            {
                levelInfo.text = text;
            }
        }

        /// <summary>
        /// Show loading screen with primary and secondary text.
        /// </summary>
        /// <param name="primaryText"></param>
        /// <param name="secondaryText"></param>
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

        /// <summary>
        /// Get canvas game object.
        /// </summary>
        /// <returns></returns>
        protected GameObject GetCanvas()
        {
            var canvas = Canvas ?? GameObject.Find("Canvas");

            if (canvas == null)
            {
                throw new InvalidOperationException($"Canvas was not found. Please set the {nameof(Canvas)} variable of the GameManager");
            }

            return canvas;
        }

        /// <summary>
        /// Hide loading screen.
        /// </summary>
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
            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        public virtual void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneFinishedLoading;
        }
    }
}