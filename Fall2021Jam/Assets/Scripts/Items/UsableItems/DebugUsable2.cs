using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable/DebugUsable2")]
public class DebugUsable2 : Item, IUsableItem
{
    public void ItemAction()
    {
        Debug.Log("Used item 2!");
    }
}
