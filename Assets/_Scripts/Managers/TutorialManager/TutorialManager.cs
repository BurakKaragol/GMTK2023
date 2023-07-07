using UnityEngine;

namespace MrLule.Managers.TutorialMan
{
    public class TutorialManager : Manager
    {


        public static TutorialManager Instance;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {

        }

        public override void OnEnable()
        {
            tutorialManager = this;
        }

        public override void OnDisable()
        {
            tutorialManager = null;
        }
    }
}
