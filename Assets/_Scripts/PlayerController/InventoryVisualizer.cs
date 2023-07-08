using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StringSprite
{
    public string itemName;
    public Sprite itemVisual;
}

[Serializable]
public class ItemVariations
{
    [SerializeField] private List<StringSprite> itemVariations = new List<StringSprite>();

    public Sprite GetSprite(string name)
    {
        for (int i = 0; i < itemVariations.Count; i++)
        {
            if (itemVariations[i].itemName == name)
            {
                return itemVariations[i].itemVisual;
            }
        }
        return null;
    }
}

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private ItemVariations itemVariations;
    [SerializeField] private Image[] images;

    private ControlManager player;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        player = FindObjectOfType<ControlManager>();
        canvasGroup = GetComponent<CanvasGroup>();
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        int inventoryCount = player.inventory.Count;

        if (inventoryCount <= 0)
        {
            canvasGroup.DOFade(0f, 1f);
            transform.DOMoveY(-30, 1f);
        }
        else
        {
            canvasGroup.DOFade(1f, 1f);
            transform.DOMoveY(30, 1f);
        }

        for (int i = 0; i < images.Length; i++)
        {
            if (i < inventoryCount)
            {
                images[i].gameObject.SetActive(true);
                images[i].sprite = itemVariations.GetSprite(player.inventory[i]);
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
}
