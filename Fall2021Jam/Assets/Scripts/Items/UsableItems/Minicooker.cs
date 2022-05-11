using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable/Minicooker")]
// Minicooker item
// Consumes 3 random ingredients from inventory in exchange for a heal
public class Minicooker : Item, IUsableItem
{
    public int ingredientsRequired;
    public float healAmount;
    private int ingredientCount = 0;
    private Player player;
    int GetIngredientCount(ItemContainer inventory) {
        int count = 0;
        foreach (Slot slot in inventory.Slots) 
            if (slot.SlotItem.BaseItem is Ingredient) 
                count += slot.amount;
        return count;
    }

    public void RemoveItems(int count, ItemContainer inventory) {
        // Choosing a random item: we know the total ingredient count, so we choose a random index to stop at. 
        // This might be in the middle of a stack, so walk over the inventory properly.
        // This also might be unnecessary, but the easier alt to choose a random index and delete will
        // make 1-stacks more vulnerable (and a lot of items or rares might be 1-stacks).
        for (int i = 0; i < count; i++) {
            int chosen = Random.Range(0, ingredientCount);
            int counted = 0;
            int index = 0;  // keeps track of which index we will remove from
            while (counted <= chosen) {
                if (inventory.Slots[index].SlotItem.BaseItem is Ingredient) counted += inventory.Slots[index].amount;
                if (counted <= chosen) index++;
            }
            inventory.RemoveItem(index);
            player.InventoryUI.Refresh();
            ingredientCount--;
        }
    }

    public void ItemAction(Player player) {
        this.player = player;
        // If player has fewer than 3 ingredients, don't activate
        ingredientCount = GetIngredientCount(player.Inventory);
        if (ingredientCount < ingredientsRequired) return;

        // Remove three random items from the player's inventory, then heal player
        RemoveItems(ingredientsRequired, player.Inventory);
        player.ApplyHeal(healAmount);
    }
}
