using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class for containers of items
/// Boxes, player inventory, etc.
/// I think? that this should be a data class that's not a MB or SO.
///     1. Player inventory, "special chests," etc probably want a MB attached to it that uses this class.
///     2. "Generic chests," i.e. ones that have random loot inside it, probably need to follow a LootTable (SO) that populates 
/// </summary>
[System.Serializable]
public class ItemContainer
{
    [System.Serializable]
    public class Slot
    {
        public Item item;
        public int amount = 0;

        public Slot(Item item) { this.item = item; this.amount = 1; }
    }

    [SerializeField] 
    private List<Slot> slots;

    [SerializeField] 
    private int containerCapacity;

    /// <summary>
    /// Adds an item to the item container.
    /// First checks if it can stack on another item, then 
    /// </summary>
    /// <param name="item"></param>
    /// <returns> true if adding succeeded, false if failed (i.e. no space for the item) </returns>
    public bool AddItem(Item item)
    {
        // First, attempt to find a slot to stack on
        foreach (Slot slot in slots) {
            if (slot.amount < slot.item.GetStackCapacity() && slot.item == item) {
                slot.amount++;
                return true;
            }
        }

        // If this is reached, nothing was found to stack -- try adding another slot
        if (slots.Count >= containerCapacity) return false; // fail if continer is full in terms of slots

        Slot newSlot = new Slot(item);
        slots.Add(newSlot);
        return true;
    }

    public void RemoveItem()
    {

    }
}
