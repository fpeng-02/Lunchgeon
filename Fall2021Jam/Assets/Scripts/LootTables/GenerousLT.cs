using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot Tables/Generous")]
public class GenerousLT : LootTable
{
    public override List<Item> GiveItems()
    {
        List<Item> dropList = new List<Item>();

        foreach (Loot drop in possibleDrops) {
            float drawn = Random.Range(0f, 1.0f);
            if (drawn <= drop.chance) {
                int quantity = RandomQuantity(drop);
                for (int i = 0; i < quantity; i++) dropList.Add(drop.item);
            }
        }
        return dropList;
    }
}
