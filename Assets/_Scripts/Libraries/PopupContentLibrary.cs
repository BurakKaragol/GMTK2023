using MrLule.Managers.PopupMan;
using UnityEngine;

namespace MrLule.Libraries
{
    [CreateAssetMenu(fileName = "PopupContentLibrary", menuName = "Libraries/Popup Content Library")]
    public class PopupContentLibrary : ScriptableObject
    {
        private static PopupContentLibrary instance;
        public static PopupContentLibrary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<PopupContentLibrary>("PopupContentLibrary");
                }
                return instance;
            }
        }

        public PopupContentSO areYouSurePopup;
    }
}
