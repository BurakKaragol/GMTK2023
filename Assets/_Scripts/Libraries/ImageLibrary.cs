using UnityEngine;

namespace MrLule.Libraries
{
    [CreateAssetMenu(fileName = "ImageLibrary", menuName = "Libraries/Image Library")]
    public class ImageLibrary : ScriptableObject
    {
        private static ImageLibrary instance;
        public static ImageLibrary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<ImageLibrary>("ImageLibrary");
                }
                return instance;
            }
        }

        /// <summary>
        /// Add all the effect prefabs here and place the ImageLibrary in the Resources folder.
        /// From anywhere in the project, you can call ImageLibrary.Instance.clickEffect to get the prefab.
        /// </summary>

        public Sprite emptyImage;
    }
}
