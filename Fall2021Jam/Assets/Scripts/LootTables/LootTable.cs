using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LootTable : ScriptableObject
{
    /// <summary>
    /// Data class representing "how" enemies drop items
    /// Serializable makes it so when you see the possibleDrops list in the Inspector,
    /// ad
    /// </summary>
    [System.Serializable]
    public class Loot
    {
        public Item item;
        public float chance;
        public int minQuantity;
        public int maxQuantity;
    }

    // Array of possible item drops; Loot class has info on drop rates, etc.
    [SerializeField] protected Loot[] possibleDrops;      

    /// <summary>
    /// Procedure for actually dropping items.
    /// Not sure if we have a specific scheme in mind, and we might want multiple different schemes,
    /// so I'm leaving this as an abstract method.
    /// </summary>
    /// <returns> ArrayList (of type Item) detailing which items were dropped </returns>
    public abstract List<ItemInstance> GiveItems();

    /// <summary>
    /// Helper to give a random quantity of a drop.
    /// I *think* it doesn't need to be more complicated than this,
    /// but open to suggestions if any.
    /// </summary>
    /// <param name="loot"> The type of loot to generate a random quantity of</param>
    /// <returns> The random quantity </returns>
    public int RandomQuantity(Loot loot)
    {
        return Random.Range(loot.minQuantity, loot.maxQuantity);
    }
}
