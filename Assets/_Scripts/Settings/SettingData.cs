using MrLule.Attributes;
using MrLule.ExtensionMethods;
using MrLule.Managers.PlayerPrefsMan;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.GlobalVariables;
using UnityEngine.UI;

namespace MrLule.Settings
{
    public abstract class SettingData<Type> : MonoBehaviour
    {
        [Header("General:")]
        [Tooltip("For saving and loading the player preferences this nema shoul be unique")]
        [SerializeField] protected string settingName;
        [SerializeField] protected bool useLocalization = true;
        [SerializeField] protected string[] strings;
        [SerializeField] protected LocalizedString localizedString;
        [SerializeField][ShowOnly] protected Type[] options;
        [SerializeField] protected UnityEvent<SettingData<Type>> onSelectedEvent;
        [SerializeField] protected UnityEvent<SettingData<Type>> onFocusEvent;

        [Header("Visuals:")]
        [SerializeField] protected TextMeshProUGUI optionText;
        [SerializeField] protected Image changedVisual;
        [SerializeField] protected Image previousButtonImage;
        [SerializeField] protected Image nextButtonImage;
        [SerializeField] protected Color normalColor = new Color(1, 1, 1, 0.6f);
        [SerializeField] protected Color disabledColor = new Color(0.6f, 0.6f, 0.6f, 0.6f);

        [SerializeField][ShowOnly] protected Type selectedValue;
        [SerializeField][ShowOnly] protected Type originalValue;
        protected bool isSelected;
        protected bool isFocused;
        protected bool hasChanged = false;
        protected bool previousButtonInteractable = true;
        protected bool nextButtonInteractable = true;
        protected int selectedValueIndex = 0;
        protected int originalValueIndex = 0;

        public Type SelectedValue
        {
            get { return selectedValue; }
            set { selectedValue = value; }
        }

        public Type OriginalValue
        {
            get { return originalValue; }
            set { originalValue = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (isSelected)
                {
                    onSelectedEvent?.Invoke(this);
                }
            }
        }

        public bool IsFocused
        {
            get { return isFocused; }
            set
            {
                isFocused = value;
                if (isFocused)
                {
                    onFocusEvent?.Invoke(this);
                }
            }
        }

        protected virtual void Start()
        {
            LoadFromPlayerPrefs();
            InitializeOptions();
            InitializeVisuals();
        }

        public void NextOption()
        {
            SetOption(ClipIndex(selectedValueIndex + 1));
        }

        public void PreviousOption()
        {
            SetOption(ClipIndex(selectedValueIndex - 1));
        }

        public int ClipIndex(int index)
        {
            index = index < 0 ? 0 : index;
            index = index >= options.Length ? options.Length - 1 : index;
            return index;
        }

        public virtual void SetOption(int index)
        {
            selectedValueIndex = index;
            selectedValue = GetOptionType(index);
            hasChanged = selectedValueIndex != originalValueIndex;
            InitializeVisuals();
        }

        public virtual Type GetOptionType(int index)
        {
            return options[index];
        }

        [InspectorButton("Apply")]
        public virtual void ApplyChanges()
        {
            if (!hasChanged)
            {
                return;
            }
            originalValue = selectedValue;
            originalValueIndex = selectedValueIndex;
            hasChanged = false;
            SaveToPlayerPrefs();
            InitializeVisuals();
        }

        [InspectorButton("Revert")]
        public void RevertChanges()
        {
            if (!hasChanged)
            {
                return;
            }
            selectedValue = originalValue;
            selectedValueIndex = originalValueIndex;
            hasChanged = false;
            InitializeVisuals();
        }

        protected virtual void LoadFromPlayerPrefs()
        {
            selectedValueIndex = originalValueIndex = PlayerPrefsManager.GetInt($"{settingName}_value", -1);
        }

        protected virtual void SaveToPlayerPrefs()
        {
            PlayerPrefsManager.SetInt($"{settingName}_value", originalValueIndex);
        }

        protected virtual void InitializeVisuals()
        {
            if (useLocalization)
            {
                string localized = GetLocalizedString(strings[selectedValueIndex]);
                if (localized.IsNullOrEmpty())
                {
                    Debug.Log($"Error on {strings[selectedValueIndex]}");
                }
                optionText.SetText(localized);
            }
            else
            {
                optionText.SetText(strings[selectedValueIndex]);
            }
            changedVisual.color = hasChanged ? normalColor : disabledColor;
            InitializeButtons();
        }

        public void InitializeButtons()
        {
            previousButtonInteractable = selectedValueIndex > 0;
            previousButtonImage.color = previousButtonInteractable ? normalColor : disabledColor;

            nextButtonInteractable = selectedValueIndex < options.Length - 1;
            nextButtonImage.color = nextButtonInteractable ? normalColor : disabledColor;
        }

        public abstract void InitializeOptions();

        protected string GetLocalizedString(string key)
        {
            localizedString.TableEntryReference = key;

            if (localizedString.GetLocalizedString().IsNotNullOrEmpty())
            {
                return localizedString.GetLocalizedString();
            }
            else
            {
                return "KEY_ERROR";
            }
        }
    }
}
