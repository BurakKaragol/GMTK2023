using UnityEngine;

namespace MrLule.Systems.TooltipSystem
{
    public class TooltipSystem : MonoBehaviour
    {
        /// <summary>
        /// TODO:
        /// remake for more detailed tooltips
        /// allow user to add more segments, stats, little images, etc.
        /// </summary>

        [Header("General:")]
        [Space(5)]
        [SerializeField] private Tooltip tooltip;

        public static TooltipSystem current;

        private void Awake()
        {
            current = this;
        }

        public static void Show(TooltipContentData[] contents, string header = "")
        {
            current.tooltip.SetText(contents, header);
            current.tooltip.gameObject.SetActive(true);
        }

        public static void Hide()
        {
            current.tooltip.gameObject.SetActive(false);
        }
    }
}
