using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    [field: SerializeField]
    public Item BaseItem {get; set;}
    public List<ItemMod> mods;

    // Constructor for an item instance without special mods
    public ItemInstance(Item baseItem) {
        this.BaseItem = baseItem;
        this.mods = new List<ItemMod>();
    }

    // Constructor for an item instance with special mods
    public ItemInstance(Item baseItem, List<ItemMod> mods) {
        this.BaseItem = baseItem;
        this.mods = mods;
    }
}
