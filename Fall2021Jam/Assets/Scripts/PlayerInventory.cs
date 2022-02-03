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
    [field: SerializeField]
    public ItemContainer InventoryContainer {get; private set;}

    [SerializeField]
    private int playerInventorySize;  // just in case this is needed; this number isn't actually used usually but should equal the capacity of the InventoryContainer

    void OnCreate()
    {
        if (InventoryContainer == null) InventoryContainer = new ItemContainer(playerInventorySize);
    }
}
