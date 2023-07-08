using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileChanger : MonoBehaviour
{
    [SerializeField] private float waitTime = 10f;
    [SerializeField] private Image fill;

    [SerializeField] private GameObject profile1;
    [SerializeField] private GameObject profile2;

    private float startTime;
    private bool isWaiting = false;
    private bool profile1Active = true;

    private void Update()
    {
        if (!isWaiting)
        {
            return;
        }

        fill.fillAmount = (Time.time - startTime) / waitTime;

        if (Time.time >= startTime + waitTime)
        {
            startTime = Time.time;
            if (profile1Active)
            {
                profile1.SetActive(false);
                profile2.SetActive(true);
                profile1Active = false;
            }
            else
            {
                profile1.SetActive(true);
                profile2.SetActive(false);
                profile1Active = true;
            }
        }
    }

    private void OnEnable()
    {
        isWaiting = true;
        startTime = Time.time;
    }

    private void OnDisable()
    {
        isWaiting = false;
        startTime = float.PositiveInfinity;
    }
}
