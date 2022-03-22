using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    [field: SerializeField]
    public ItemInstance SlotItem {get; set;}
    public int amount = 0;

    public Slot(ItemInstance item) { this.SlotItem = item; this.amount = 1; }
}