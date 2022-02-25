using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject inventorySlotGo;
    [SerializeField] private GameObject itemDropGo;
    [SerializeField] private float pickupCooldown;

    private int currentSelect = -1; // -1 means nothing is highlighted
    private List<PlayerInventorySlotUI> slotUIs;

    // Responds to UI request to remove a single item, updates PlayerInventory SO accordingly
    public void RemoveItem(int idx)
    {
        Transform playerPosition = GameObject.Find("Player").transform;
        GameObject go = Instantiate(itemDropGo, playerPosition.position, Quaternion.identity);
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        go?.GetComponent<ItemDrop>()?.InitializeItemDrop(slots[idx].SlotItem, pickupCooldown);

        int oldAmount = slots[idx].amount; // do this *before* the data update in playerInventory
        playerInventory.InventoryContainer.RemoveItem(idx);
        if (oldAmount == 1) RegenerateSlots();  // kind of assumes previous line was successful
        else slotUIs[idx].UpdateAmount(oldAmount - 1);
    }

    // Responds to UI request to remove a stack of items, updates PlayerInventory SO accordingly
    public void RemoveStack(int idx)
    {
        Transform playerPosition = GameObject.Find("Player").transform;
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        for (int i = 0; i < slots[idx].amount; i++)
        {
            GameObject go = Instantiate(itemDropGo, playerPosition.position, Quaternion.identity);
            go?.GetComponent<ItemDrop>()?.InitializeItemDrop(slots[idx].SlotItem, pickupCooldown);
        }
        playerInventory.InventoryContainer.RemoveStack(idx);
        RegenerateSlots();
    }

    // Selects slot by index, "equip" or "drop" keys respond to newly selected slot
    public void SelectSlot(int idx)
    {
        if (currentSelect >= 0) slotUIs[currentSelect].DisableHighlight();
        slotUIs[idx].EnableHighlight();
        this.currentSelect = idx;
    }

    public void RegenerateSlots()
    {
        // Destroy children, remake slots; basically refreshes. 
        // Maybe a bit expensive on paper, but there shouldn't be too many items.
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // On a refresh, maybe the count of slots decreased, so selected might be off --- just reset it
        currentSelect = -1;

        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        int index = 0;
        (slotUIs ??= new List<PlayerInventorySlotUI>()).Clear();
        foreach (Slot slot in slots)
        {
            GameObject instGo = Instantiate(inventorySlotGo);
            instGo.transform.SetParent(this.transform);
            PlayerInventorySlotUI slotUI = instGo?.GetComponent<PlayerInventorySlotUI>();
            slotUI?.InitializeUISlot(slot, index, this);
            slotUIs.Add(slotUI);
            index++;
        }
    }

    // Just a nicer name that other classes can call
    public void Refresh() { RegenerateSlots(); }

    public void DropSelectedItem() {
        if (currentSelect < 0) return;
        if (Input.GetKey(KeyCode.LeftControl)) RemoveStack(currentSelect); // Not sure if it should just be stuck to LCTRL
        else RemoveItem(currentSelect);
    }

    public void EquipItemData() {
        // take out the selected item
        List<Slot> slots = playerInventory.InventoryContainer.Slots;
        if (currentSelect < 0 || !(slots[currentSelect].SlotItem.BaseItem is IUsableItem)) return;
        ItemInstance takenOut = slots[currentSelect].SlotItem;
        Debug.Log(takenOut);

        // put the current/old active item back into the inventory
        if (playerInventory.ActiveItem != null)
            playerInventory.InventoryContainer.AddItem(playerInventory.ActiveItem);
        playerInventory.ActiveItem = takenOut;

        playerInventory.InventoryContainer.RemoveItem(currentSelect);
        RegenerateSlots();
    }
}
