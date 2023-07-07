using UnityEngine;

namespace MrLule.Managers.AnalyticsMan
{
    public class AnalyticsManager : Manager
    {
        /// <summary>
        /// Track the data of the player
        /// How much time they spent on the game
        /// How many times the they are died
        /// How many times they jumped
        /// How much distance they walked/runned
        /// </summary>

        public static AnalyticsManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public override void OnEnable()
        {
            analyticsManager = this;
        }

        public override void OnDisable()
        {
            analyticsManager = null;
        }
    }
}
