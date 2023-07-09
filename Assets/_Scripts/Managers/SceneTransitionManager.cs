using MrLule.Attributes;
using MrLule.General;
using MrLule.Managers.PlayerPrefsMan;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

namespace MrLule.Managers.SceneTransitionMan
{
    [Serializable]
    public class TransitionData
    {
        [Tooltip("Name of the transition. NextScene(name)")]
        public string name;
        public Animator animator;
        public string trigger = "Start";
        public float animationTime = 2f;
    }

    public class SceneTransitionManager : Manager
    {
        [SerializeField] private int sceneLoaderIndex = 2;

        [Header("Transitions:")]
        [SerializeField] private string defaultTransitionName = "Alpha";
        [SerializeField] private TransitionData[] transitions;

        [ShowOnly] public float loadingProgress = 0;

        private AsyncOperation asyncOperation;

        public bool isPaused { get; private set; }
        public Scene activeScene { get; private set; }

        public static SceneTransitionManager Instance;
        private LoadingSceneSystem loadingSceneSystem;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            asyncOperation = new AsyncOperation();
        }

        private void Start()
        {
            activeScene = SceneManager.GetActiveScene();

            if (activeScene.buildIndex == sceneLoaderIndex) // we are at the loading scene
            {
                int sceneToLoad = PlayerPrefs.GetInt("SceneToLoad", sceneLoaderIndex + 1);
                if (sceneToLoad == -1)
                {
                    Debugger.LogWarning("SceneTransitionManager", "Cannot load scene (Scene index -1)");
                    return;
                }
                else
                {
                    asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
                    asyncOperation.allowSceneActivation = false;
                    loadingSceneSystem = FindObjectOfType<LoadingSceneSystem>();
                    if (loadingSceneSystem)
                    {
                        StartCoroutine(TrackLoadingProgress());
                    }
                }
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
        }

        public void PauseScene()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }

        public void UnpauseScene()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }

        public void ContinueScene()
        {
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1f;
        }

        public void LoadScene(int sceneIndex)
        {
            LoadScene(sceneIndex, defaultTransitionName);
        }

        public void LoadScene(int sceneIndex, string name)
        {
            if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot load scene (Index is out of bounds)");
                return;
            }

            PlayerPrefs.SetInt("SceneToLoad", sceneIndex);
            StartCoroutine(LoadLevel(sceneLoaderIndex, name));
        }

        public void LoadSceneDirectly(int sceneIndex)
        {
            LoadSceneDirectly(sceneIndex, defaultTransitionName);
        }

        public void LoadSceneDirectly(int sceneIndex, string name)
        {
            if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot load scene (Index is out of bounds)");
                return;
            }
            StartCoroutine(LoadLevel(sceneIndex, name));
        }

        public void NextScene()
        {
            NextScene(defaultTransitionName);
        }

        public void NextScene(string name)
        {
            int requestedIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (requestedIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot open next scene (There is no next scene)");
                return;
            }

            if (requestedIndex == sceneLoaderIndex)
            {
                LoadScene(requestedIndex + 1, name);
            }
            else
            {
                LoadScene(requestedIndex, name);
            }
        }

        public void NextSceneDirectly()
        {
            NextSceneDirectly(defaultTransitionName);
        }

        public void NextSceneDirectly(string name)
        {
            LoadSceneDirectly(SceneManager.GetActiveScene().buildIndex + 1, name);
        }

        public void RestartScene()
        {
            RestartScene(defaultTransitionName);
        }

        public void RestartScene(string name)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex, name);
        }

        private TransitionData GetTransitionData(string name)
        {
            TransitionData transitionData = Array.Find(transitions, transition => transition.name == name);
            return transitionData;
        }

        private bool TryGetTransitionData(string name, out TransitionData transitionData)
        {
            transitionData = GetTransitionData(name);
            if (transitionData == null)
            {
                return false;
            }
            return true;
        }

        IEnumerator LoadLevel(int sceneIndex, string transitionName = null)
        {
            if (TryGetTransitionData(transitionName, out TransitionData transitionData))
            {
                ContinueScene();
                transitionData.animator.SetTrigger(transitionData.trigger);
                yield return new WaitForSeconds(transitionData.animationTime);
                SceneManager.LoadScene(sceneIndex);
            }
        }

        private IEnumerator TrackLoadingProgress()
        {
            while (!asyncOperation.isDone)
            {
                loadingProgress = (asyncOperation.progress >= 0.9f ? 1f : asyncOperation.progress) * loadingSceneSystem.waitProgress;
                if (loadingProgress >= 0.99f)
                {
                    loadingProgress = 1f;
                    loadingSceneSystem.fillImage.fillAmount = 1f;
                    if (TryGetTransitionData(defaultTransitionName, out TransitionData transitionData))
                    {
                        ContinueScene();
                        transitionData.animator.SetTrigger(transitionData.trigger);
                        yield return new WaitForSeconds(transitionData.animationTime);
                        asyncOperation.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }

        public override void OnEnable()
        {
            sceneTransitionManager = this;
        }

        public override void OnDisable()
        {
            sceneTransitionManager = null;
        }
    }
}
