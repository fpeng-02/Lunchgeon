using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlotUI : MonoBehaviour
{
    public Item SlotItem { get; set; }

    private Image image;
    private Text text;
    private int slotIndex;
    private InventorySlotsManager ism;

    public void InitializeUISlot(Slot slot, int slotIndex, InventorySlotsManager ism)
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();

        this.slotIndex = slotIndex;
        this.ism = ism;
        SlotItem = slot.SlotItem;
        image.sprite = SlotItem.ItemSprite;
        text.text = slot.amount.ToString();
    }

    public void OnClick()
    {
        if (Input.GetKey(KeyCode.LeftControl) == true)
        {
            ism.RemoveStack(this.slotIndex);
        }
        else
        {
            ism.RemoveItem(this.slotIndex);
        }

    }
}
