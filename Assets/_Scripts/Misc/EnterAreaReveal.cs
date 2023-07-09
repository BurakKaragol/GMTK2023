using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class enterAreaReveal : MonoBehaviour
{
    public string playerTag = "Player";
    public float normalValue = 1f;
    public float hideValue = 0.5f;

    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.color = new Color(1f, 1f, 1f, hideValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            tilemap.color = new Color(1f, 1f, 1f, hideValue);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            tilemap.color = new Color(1f, 1f, 1f, hideValue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            tilemap.color = new Color(1f, 1f, 1f, normalValue);
        }
    }
}
