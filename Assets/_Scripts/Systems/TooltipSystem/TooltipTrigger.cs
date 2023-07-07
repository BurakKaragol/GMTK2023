using MrLule.General;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MrLule.Systems.TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("General:")]
        [Space(5)]
        [SerializeField] private bool useUIElement = true;

        [Header("Tooltip variables:")]
        [Space(5)]
        [SerializeField] private string header = "";
        [SerializeField] private TooltipContentData[] contents;
        [SerializeField] private float waitTime = 1f;

        private float areaEnterTime;
        private bool inArea = false;

        private void Update()
        {
            if (!inArea)
            {
                SetTooltipUI(false);
                return;
            }

            if (Time.time >= areaEnterTime + waitTime)
            {
                SetTooltipUI(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            inArea = true;
            areaEnterTime = Time.time;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inArea = false;
        }

        private void SetTooltipUI(bool state)
        {
            if (!useUIElement)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot show tooltip (Use ui element i turned off)");
                return;
            }

            if (state)
            {
                TooltipSystem.Show(contents, header);
            }
            else
            {
                TooltipSystem.Hide();
            }
        }

        private void SetTooltipGO(bool state)
        {
            if (useUIElement)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot show tooltip (Use ui element i turned on)");
                return;
            }

            if (state)
            {
                TooltipSystem.Show(contents, header);
            }
            else
            {
                TooltipSystem.Hide();
            }
        }
    }
}
