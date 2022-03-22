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

    [field: SerializeField]
    public List<Slot> Slots { get; set; }

    [SerializeField]
    private int containerCapacity;


    public ItemContainer(int containerCapacity)
    {
        this.Slots = new List<Slot>();
        this.containerCapacity = containerCapacity;
    }

    /// <summary>
    /// Adds an item to the item container.
    /// First checks if it can stack on another item, then 
    /// </summary>
    /// <param name="item"></param>
    /// <returns> true if adding succeeded, false if failed (i.e. no space for the item) </returns>
    public bool AddItem(ItemInstance item)
    {
        // First, attempt to find a slot to stack on
        foreach (Slot slot in Slots)
        {
            if (slot.amount < slot.SlotItem.BaseItem.StackCapacity && slot.SlotItem.Equals(item))
            {
                slot.amount++;
                return true;
            }
        }

        // If this is reached, nothing was found to stack -- try adding another slot
        if (Slots.Count >= containerCapacity) return false; // fail if continer is full in terms of slots

        Slot newSlot = new Slot(item);
        Slots.Add(newSlot);
        return true;
    }

    public void RemoveStack(int index)
    {
        if (index >= 0 && index < Slots.Count)
        {
            Slots.RemoveAt(index);
        }
    }
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < Slots.Count)
        {
            if (Slots[index].amount == 1) RemoveStack(index);
            else Slots[index].amount--;
        }
    }
}
