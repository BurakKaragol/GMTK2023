using MrLule.Managers.AudioMan;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioManager))]
public class AudioSubtitleSystem : MonoBehaviour
{
    [SerializeField] private Color defaultSubtitleColor = Color.white;
    [SerializeField] private Color defaultBackgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
    [SerializeField] private GameObject subtitlePanel;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private Image subtitleBackground;

    public void ShowSubtitle(string subtitle, float duration, Color? subtitleColor = null, Color? backgroundColor = null)
    {
        StartCoroutine(ShowSubtitleForDuration(subtitle, duration,
            (Color)(subtitleColor == null ? defaultSubtitleColor : subtitleColor),
            (Color)(backgroundColor == null ? defaultBackgroundColor : backgroundColor)));
    }

    public void ShowSubtitle(string subtitle, float duration)
    {
        StartCoroutine(ShowSubtitleForDuration(subtitle, duration, defaultSubtitleColor, defaultBackgroundColor));
    }

    IEnumerator ShowSubtitleForDuration(string subtitle, float duration, Color subtitleColor, Color backgroundColor)
    {
        subtitleText.SetText(subtitle);
        subtitleText.color = subtitleColor;
        subtitleBackground.color = backgroundColor;
        subtitlePanel.SetActive(true);
        yield return new WaitForSeconds(duration);
        subtitlePanel.SetActive(false);
    }
}
