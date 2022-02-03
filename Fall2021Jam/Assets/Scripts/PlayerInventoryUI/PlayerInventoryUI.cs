using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject inventorySlotGo;

    public void GenerateSlots() 
    {
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        foreach (Slot slot in slots) {
            GameObject instGo = Instantiate(inventorySlotGo);
            instGo?.GetComponent<PlayerInventorySlotUI>()?.InitializeUISlot(slot);
            instGo.transform.SetParent(this.transform);
        }
    }

    void Start() {
        GenerateSlots();
    }
}
