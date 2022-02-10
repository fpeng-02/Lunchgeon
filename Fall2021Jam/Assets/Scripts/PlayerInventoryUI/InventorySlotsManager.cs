using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject inventorySlotGo;

    public void RemoveItem(int slotIndex) {
        playerInventory.InventoryContainer.RemoveItem(slotIndex);
        RegenerateSlots();
    }

    public void RemoveStack(int slotIndex) {
        playerInventory.InventoryContainer.RemoveStack(slotIndex);
        RegenerateSlots();
    }

    public void RegenerateSlots() 
    {
        // Destroy children, remake slots; basically refreshes. 
        // Maybe a bit expensive on paper, but there shouldn't be too many items.
        foreach (Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        int index = 0;
        foreach (Slot slot in slots) {
            GameObject instGo = Instantiate(inventorySlotGo);
            instGo?.GetComponent<PlayerInventorySlotUI>()?.InitializeUISlot(slot, index, this);
            instGo.transform.SetParent(this.transform);
            index++;
        }
    }
}
