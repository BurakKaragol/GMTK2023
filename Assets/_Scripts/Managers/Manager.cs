using UnityEngine;
using MrLule.Managers.AchievemenMan;
using MrLule.Managers.AnalyticsMan;
using MrLule.Managers.AudioMan;
using MrLule.Managers.CameraMan;
using MrLule.Managers.CheckpointMan;
using MrLule.Managers.CursorMan;
using MrLule.Managers.DamagePopupMan;
using MrLule.Managers.DialogueMan;
using MrLule.Managers.DifficultyMan;
using MrLule.Managers.GameMan;
using MrLule.Managers.InputMan;
using MrLule.Managers.MainMenuMan;
using MrLule.Managers.MessageMan;
using MrLule.Managers.PlayerBehaviourMan;
using MrLule.Managers.PlayerPrefsMan;
using MrLule.Managers.PopupMan;
using MrLule.Managers.QuestMan;
using MrLule.Managers.TimelineMan;
using MrLule.Managers.TutorialMan;
using MrLule.Managers.SceneTransitionMan;
using MrLule.Managers.ScreenBorderMan;

namespace MrLule.Managers
{
    public abstract class Manager : MonoBehaviour
    {
        public static AchievementManager achievementManager
        {
            get => _achievementManager == null ? _achievementManager = FindObjectOfType<AchievementManager>() : _achievementManager;
            set => _achievementManager = value;
        }
        private static AchievementManager _achievementManager;

        public static AnalyticsManager analyticsManager
        {
            get => _analyticsManager == null ? _analyticsManager = FindObjectOfType<AnalyticsManager>() : _analyticsManager;
            set => _analyticsManager = value;
        }
        private static AnalyticsManager _analyticsManager;

        public static AudioManager audioManager
        {
            get => _audioManager == null ? _audioManager = AudioManager.Instance : _audioManager;
            set => _audioManager = value;
        }
        private static AudioManager _audioManager;

        public static CameraManager cameraManager
        {
            get => _cameraManager == null ? _cameraManager = FindObjectOfType<CameraManager>() : _cameraManager;
            set => _cameraManager = value;
        }
        private static CameraManager _cameraManager;

        public static CheckpointManager checkpointManager
        {
            get => _checkpointManager == null ? _checkpointManager = FindObjectOfType<CheckpointManager>() : _checkpointManager;
            set => _checkpointManager = value;
        }
        private static CheckpointManager _checkpointManager;

        public static CursorManager cursorManager
        {
            get => _cursorManager == null ? _cursorManager = FindObjectOfType<CursorManager>() : _cursorManager;
            set => _cursorManager = value;
        }
        private static CursorManager _cursorManager;

        public static DamagePopupManager damagePopupManager
        {
            get => _damagePopupManager == null ? _damagePopupManager = FindObjectOfType<DamagePopupManager>() : _damagePopupManager;
            set => _damagePopupManager = value;
        }
        private static DamagePopupManager _damagePopupManager;

        public static DialogueManager dialogueManager
        {
            get => _dialogueManager == null ? _dialogueManager = FindObjectOfType<DialogueManager>() : _dialogueManager;
            set => _dialogueManager = value;
        }
        private static DialogueManager _dialogueManager;

        public static DifficultyManager difficultyManager
        {
            get => _difficultyManager == null ? _difficultyManager = FindObjectOfType<DifficultyManager>() : _difficultyManager;
            set => _difficultyManager = value;
        }
        private static DifficultyManager _difficultyManager;

        public static GameManager gameManager
        {
            get => _gameManager == null ? _gameManager = FindObjectOfType<GameManager>() : _gameManager;
            set => _gameManager = value;
        }
        private static GameManager _gameManager;

        public static InputManager inputManager
        {
            get => _inputManager == null ? _inputManager = FindObjectOfType<InputManager>() : _inputManager;
            set => _inputManager = value;
        }
        private static InputManager _inputManager;

        public static MainMenuManager mainMenuManager
        {
            get => _mainMenuManager == null ? _mainMenuManager = FindObjectOfType<MainMenuManager>() : _mainMenuManager;
            set => _mainMenuManager = value;
        }
        private static MainMenuManager _mainMenuManager;

        public static MessageManager messageManager
        {
            get => _messageManager == null ? _messageManager = FindObjectOfType<MessageManager>() : _messageManager;
            set => _messageManager = value;
        }
        private static MessageManager _messageManager;

        public static PlayerBehaviourManager playerBehaviourManager
        {
            get => _playerBehaviourManager == null ? _playerBehaviourManager = FindObjectOfType<PlayerBehaviourManager>() : _playerBehaviourManager;
            set => _playerBehaviourManager = value;
        }
        private static PlayerBehaviourManager _playerBehaviourManager;

        public static PlayerPrefsManager playerPrefsManager
        {
            get => _playerPrefsManager == null ? _playerPrefsManager = FindObjectOfType<PlayerPrefsManager>() : _playerPrefsManager;
            set => _playerPrefsManager = value;
        }
        private static PlayerPrefsManager _playerPrefsManager;

        public static PopupManager popupManager
        {
            get => _popupManager == null ? _popupManager = FindObjectOfType<PopupManager>() : _popupManager;
            set => _popupManager = value;
        }
        private static PopupManager _popupManager;

        public static QuestManager questManager
        {
            get => _questManager == null ? _questManager = FindObjectOfType<QuestManager>() : _questManager;
            set => _questManager = value;
        }
        private static QuestManager _questManager;

        public static TimelineManager timelineManager
        {
            get => _timelineManager == null ? _timelineManager = FindObjectOfType<TimelineManager>() : _timelineManager;
            set => _timelineManager = value;
        }
        private static TimelineManager _timelineManager;

        public static TutorialManager tutorialManager
        {
            get => _tutorialManager == null ? _tutorialManager = FindObjectOfType<TutorialManager>() : _tutorialManager;
            set => _tutorialManager = value;
        }
        private static TutorialManager _tutorialManager;

        public static SceneTransitionManager sceneTransitionManager
        {
            get => _sceneTransitionManager == null ? _sceneTransitionManager = FindObjectOfType<SceneTransitionManager>() : _sceneTransitionManager;
            set => _sceneTransitionManager = value;
        }
        private static SceneTransitionManager _sceneTransitionManager;

        public static ScreenBorderManager screenBorderManager
        {
            get => _screenBorderManager == null ? _screenBorderManager = FindObjectOfType<ScreenBorderManager>() : _screenBorderManager;
            set => _screenBorderManager = value;
        }
        private static ScreenBorderManager _screenBorderManager;

        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}
