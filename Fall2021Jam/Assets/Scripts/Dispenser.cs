using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemContainer itemContainer;

    [SerializeField]
    private Sprite unpickedSprite;
    [SerializeField]
    private Sprite pickedSprite;

    private bool picked = false;

    private SpriteRenderer sr;

    [SerializeField]
    private GameObject itemDropGo;

    // Interact with the berry bush => drop the berries
    public void OnInteract()
    {
        if (!picked)
        {
            picked = true;
            sr.sprite = pickedSprite;
            // Drop berries on the ground: for each slot in the ItemContainer, generate a GameObject
            // can make prettier: spread out items a bit, maybe do a drop animation
            foreach (Slot dropSlot in itemContainer.Slots)
            {
                for (int i = 0; i < dropSlot.amount; i++)
                {
                    GameObject go = Instantiate(itemDropGo, this.transform.position, Quaternion.identity);
                    ItemDrop itemDrop = go.GetComponent<ItemDrop>();
                    itemDrop?.InitializeItemDrop(new ItemInstance(dropSlot.SlotItem.BaseItem));
                }
            }
        }
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = unpickedSprite;
    }
}
