using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    [field: SerializeField]
    public Item BaseItem { get; set; }
    public List<ItemMod> mods;

    // Constructor for an item instance without special mods
    public ItemInstance(Item baseItem)
    {
        this.BaseItem = baseItem;
        this.mods = new List<ItemMod>();
    }

    // Constructor for an item instance with special mods
    public ItemInstance(Item baseItem, List<ItemMod> mods)
    {
        this.BaseItem = baseItem;
        this.mods = mods;
    }

    // May need improvement especially if we use ItemInstances in a dictionary/hashmap
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    // Custom equals compare method so stacking ItemInstances works properly
    public override bool Equals(System.Object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            // may need to compare mods when we implement them
            // comparing mods would probably look like a for loop that iterates over and compares values of ItemMod
            // (comparing reference of List using something like tI.mods == this.mods obviously doesn't work..)
            ItemInstance testInstance = (ItemInstance)obj;
            return testInstance.BaseItem == this.BaseItem;
        }
    }
}
