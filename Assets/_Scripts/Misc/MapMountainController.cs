using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapMountainController : MonoBehaviour
{
    [SerializeField] private Tilemap[] objectstoActivate;
    [SerializeField] private Tilemap[] objectsToDeactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Tilemap obj in objectstoActivate)
        {
            obj.color = new Color(1, 1, 1, 1);
            if (obj.TryGetComponent(out enterAreaReveal areaReveal))
            {
                areaReveal.enabled = true;
            }
            else
            {
                obj.GetComponent<TilemapCollider2D>().enabled = true;
            }
        }
        foreach (Tilemap obj in objectsToDeactivate)
        {
            obj.color = new Color(0.25f, 0.25f, 0.25f, 0.25f);
            if (obj.TryGetComponent(out enterAreaReveal areaReveal))
            {
                areaReveal.enabled = false;
            }
            else
            {
                obj.GetComponent<TilemapCollider2D>().enabled = false;
            }
        }
    }
}
