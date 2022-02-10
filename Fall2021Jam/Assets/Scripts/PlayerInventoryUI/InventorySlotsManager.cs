using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject inventorySlotGo;
    [SerializeField] private GameObject itemDropGo;
    [SerializeField] private float pickupCooldown;

    public void RemoveItem(int slotIndex)
    {
        Transform playerPosition = GameObject.Find("Player").transform;
        GameObject go = Instantiate(itemDropGo, playerPosition.position, Quaternion.identity);
        go?.GetComponent<ItemDrop>()?.InitializeItemDrop(playerInventory.InventoryContainer.Slots[slotIndex].SlotItem, pickupCooldown);
        playerInventory.InventoryContainer.RemoveItem(slotIndex);
        RegenerateSlots();
    }

    public void RemoveStack(int slotIndex)
    {
        Transform playerPosition = GameObject.Find("Player").transform;
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        for (int i = 0; i < slots[slotIndex].amount; i++)
        {
            GameObject go = Instantiate(itemDropGo, playerPosition.position, Quaternion.identity);
            go?.GetComponent<ItemDrop>()?.InitializeItemDrop(slots[slotIndex].SlotItem, pickupCooldown);
        }
        playerInventory.InventoryContainer.RemoveStack(slotIndex);
        RegenerateSlots();
    }

    public void RegenerateSlots()
    {
        // Destroy children, remake slots; basically refreshes. 
        // Maybe a bit expensive on paper, but there shouldn't be too many items.
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        int index = 0;
        foreach (Slot slot in slots)
        {
            GameObject instGo = Instantiate(inventorySlotGo);
            instGo?.GetComponent<PlayerInventorySlotUI>()?.InitializeUISlot(slot, index, this);
            instGo.transform.SetParent(this.transform);
            index++;
        }
    }
}
