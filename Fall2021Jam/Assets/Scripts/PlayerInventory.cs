using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is really just a wrapper of an ItemContainer for the player.
/// SOs make it nice to serialize and save (for us, esp. after play mode ends)
/// </summary>
[CreateAssetMenu]
public class PlayerInventory : ScriptableObject
{
    [SerializeField]
    private ItemContainer slots;

    [SerializeField]
    private int playerInventorySize;

    void OnCreate()
    {
        if (slots == null) slots = new ItemContainer(playerInventorySize);
    }

    public ItemContainer GetItemContainer() { return slots; }
}
