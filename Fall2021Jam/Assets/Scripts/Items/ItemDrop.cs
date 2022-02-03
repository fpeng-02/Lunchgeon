using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for GameObjects that spawn when enemies die.
/// These objects are trigger colliders; when the *player* steps on it, try to add this item to their inventory.
/// </summary>
public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    private Item item;  // the actual item data the GO is holding
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ItemDrops are going to be Instantiated upon enemy deaths; 
    /// </summary>
    /// <param name="item"></param>
    public void InitializeItemDrop(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.ItemSprite;
    }

    /// <summary>
    /// First check if the *player* is trying to pick up the item by checking the collider.
    /// On pickup, try to add the item to the player's inventory.
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.gameObject.CompareTag("Player")) {
            Player player = col.transform.gameObject.GetComponent<Player>();
            ItemContainer playerInventory = player.Inventory;
            if (playerInventory.AddItem(item)) 
            {
                Destroy(gameObject);  // destroy the item drop only if adding the item succeeded
                player.InventoryUI.Refresh();
            }
        }
    }

    /// <summary>
    /// The exact same functionality as OnTriggerEnter
    /// Duplicated because the player make space in their inventory while standing on a dropped item.
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.gameObject.CompareTag("Player")) {
            Player player = col.transform.gameObject.GetComponent<Player>();
            ItemContainer playerInventory = player.Inventory;
            if (playerInventory.AddItem(item)) 
            {
                Destroy(gameObject);  // destroy the item drop only if adding the item succeeded
                player.InventoryUI.Refresh();
            }
        }
    }
}
