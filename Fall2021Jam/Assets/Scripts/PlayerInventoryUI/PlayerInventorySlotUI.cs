using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlotUI : MonoBehaviour
{
    public Item SlotItem {get; set;}

    private Image image;
    private Text text;

    public void InitializeUISlot(Slot slot)
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();

        SlotItem = slot.SlotItem;
        image.sprite = SlotItem.ItemSprite;
        text.text = slot.amount.ToString();
    }
}