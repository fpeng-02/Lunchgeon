using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField]
    private LootTable lootTable;

    [SerializeField]
    private GameObject itemDropGo;

    private RoomEvent roomEvent;

    public void SetRoomEvent(RoomEvent re) { this.roomEvent = re; }

    public override void Die()
    {
        DoLootDrops();
        roomEvent.ProgressRoom();
        base.Die();
    }

    /// <summary>
    /// Performs loot drops on death.
    /// First, gets a list of items to drop (abstract, data stuff from LootTable)
    /// then makes GameObjects that spawn on the ground for the player to pick up.
    /// </summary>
    private void DoLootDrops()
    {
        if (lootTable == null) return;  // null loot table = no drops, ever (!)
        List<ItemInstance> toDrop = lootTable.GiveItems();
        foreach (ItemInstance drop in toDrop) {
            GameObject go = Instantiate(itemDropGo, this.transform.position, Quaternion.identity);
            ItemDrop itemDrop = go.GetComponent<ItemDrop>();
            itemDrop?.InitializeItemDrop(drop);
        }
    }
}
