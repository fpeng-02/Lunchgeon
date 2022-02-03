using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsGenerator : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject inventorySlotGo;

    public void RegenerateSlots() 
    {
        // Destroy children, remake slots; basically refreshes. 
        // Maybe a bit expensive on paper, but there shouldn't be too many items.
        foreach (Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        foreach (Slot slot in slots) {
            GameObject instGo = Instantiate(inventorySlotGo);
            instGo?.GetComponent<PlayerInventorySlotUI>()?.InitializeUISlot(slot);
            instGo.transform.SetParent(this.transform);
        }
    }
}
