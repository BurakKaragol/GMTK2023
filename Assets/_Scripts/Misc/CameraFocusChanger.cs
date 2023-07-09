using Cinemachine;
using MrLule.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Transform max;
    [SerializeField] private float maximum = 5f;
    private CinemachineFramingTransposer framingTransposer;
    private bool playerOnArea = false;
    private Transform player;

    private void Start()
    {
        framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void LateUpdate()
    {
        if (playerOnArea)
        {
            float value = Mathf.Abs(max.position.x - player.position.x) / maximum;
            framingTransposer.m_ScreenX = Mathf.Lerp(0, 0.5f, value);
        }
        else
        {
            framingTransposer.m_ScreenX = 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnArea = true;
        player = collision.transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnArea = true;
        player = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnArea = false;
        player = null;
    }
}
