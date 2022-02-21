using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public ItemInstance SlotItem { get; set; }

    [SerializeField] private Image image;
    [SerializeField] private Image highlightImage;
    [SerializeField] private Text text;
    private int slotIndex;
    private InventorySlotsManager ism;

    public void InitializeUISlot(Slot slot, int slotIndex, InventorySlotsManager ism)
    {
        DisableHighlight();
        this.slotIndex = slotIndex;
        this.ism = ism;
        this.SlotItem = slot.SlotItem;
        image.sprite = SlotItem.BaseItem.ItemSprite;
        text.text = slot.amount.ToString();
    }

    public void EnableHighlight()
    {
        Color tmp = highlightImage.color;
        tmp.a = 1.0f;
        highlightImage.color = tmp;
    }

    public void DisableHighlight()
    {
        Color tmp = highlightImage.color;
        tmp.a = 0.0f;
        highlightImage.color = tmp;
    }

    public void UpdateAmount(int newAmount)
    {
        text.text = newAmount.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ism.SelectSlot(slotIndex);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Input.GetKey(KeyCode.LeftControl) == true)
            {
                ism.RemoveStack(this.slotIndex);
            }
            else
            {
                ism.RemoveItem(this.slotIndex);
            }
            return;
        }
    }
}
