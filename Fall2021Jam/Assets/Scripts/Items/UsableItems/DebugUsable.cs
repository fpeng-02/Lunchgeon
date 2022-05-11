using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable/DebugUsable")]
public class DebugUsable : Item, IUsableItem
{
    public void ItemAction(Player player)
    {
        Debug.Log("Used item!");
    }
}
