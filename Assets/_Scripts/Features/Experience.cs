using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.Features
{
    public class Experience : MonoBehaviour
    {
        [Header("General:")]
        [Space(5)]
        [SerializeField] private int experience;
        [SerializeField] private int level;
        [SerializeField] private int requiredExperience;
        [SerializeField] private int changeStep;

        [Header("UI Elements:")]
        [Space(5)]
        [SerializeField] private bool useSlider;
        [SerializeField] private Slider slider;

        public Experience(int firstRequired = 10)
        {
            requiredExperience = firstRequired;
            experience = 0;
            level = 1;
        }

        public int GetExperience()
        {
            return experience;
        }

        public int GetLevel()
        {
            return level;
        }

        public float GetExperiencePercent()
        {
            return experience / requiredExperience;
        }

        public void Increase(int experienceAmount)
        {
            experience += experienceAmount;
            CheckLevel();
        }

        public void AddOverTime(int addAmount, int increaseAmount = 0)
        {
            if (increaseAmount == 0)
            {
                StartCoroutine(IncreaseOverTime(changeStep, addAmount));
            }
            else
            {
                StartCoroutine(IncreaseOverTime(increaseAmount, addAmount));
            }
        }

        private void CheckLevel()
        {
            if (experience >= requiredExperience)
            {
                experience -= requiredExperience;
                requiredExperience *= 2;
                level++;
            }
        }

        private void UpdateSlider()
        {
            if (useSlider)
            {
                slider.value = GetExperiencePercent();
            }
        }

        IEnumerator IncreaseOverTime(int increaseAmount, int target)
        {
            while (experience < target)
            {
                experience += increaseAmount;
                yield return new WaitForSeconds(0.1f);
                UpdateSlider();
            }
            experience = target;
            UpdateSlider();
            CheckLevel();
        }
    }
}
