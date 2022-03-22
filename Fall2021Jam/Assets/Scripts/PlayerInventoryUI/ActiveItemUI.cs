using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    private Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
        UpdateActiveItemUI();
    }

    public void UpdateActiveItemUI()
    {
        image = GetComponent<Image>();
        if (playerInventory.ActiveItem != null)
            image.sprite = playerInventory.ActiveItem.BaseItem.ItemSprite;
    }
}
