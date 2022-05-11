using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Empty (for now) data class for usable items.
/// May want to split this into "cooldown" items or "limited amount of uses" items
/// (or limited amount of uses with charges!) not sure.
/// 
/// (maybe can also combine both types into the same class, i.e. "cooldown = infinity" or something)
/// </summary>
public interface IUsableItem
{
    // Only the player uses items anyway..?
    // The idea is that a lot of items probably depend on the player, so it's faster to just pass than to find gameobject every time
    public void ItemAction(Player player);
}
