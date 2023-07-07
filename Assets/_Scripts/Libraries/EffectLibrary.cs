using UnityEngine;

namespace MrLule.Libraries
{
    [CreateAssetMenu(fileName = "EffectLibrary", menuName = "Libraries/Effect Library")]
    public class EffectLibrary : ScriptableObject
    {
        private static EffectLibrary instance;
        public static EffectLibrary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<EffectLibrary>("EffectLibrary");
                }
                return instance;
            }
        }

        /// <summary>
        /// Add all the effect prefabs here and place the EffectLibrary in the Resources folder.
        /// From anywhere in the project, you can call EffectLibrary.Instance.clickEffect to get the prefab.
        /// </summary>

        public GameObject clickEffect;
    }
}
