using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    private bool inventoryIsOpen;
    private InventorySlotsManager gen;

    void Start()
    {
        gen = GetComponentInChildren<InventorySlotsManager>();
        this.gameObject.SetActive(false);
    }

    public void ToggleEnable() 
    {
        if (inventoryIsOpen) {
            this.gameObject.SetActive(false);
            inventoryIsOpen = false;
        } else {
            gen.RegenerateSlots();
            this.gameObject.SetActive(true);
            inventoryIsOpen = true;
        }
    }

    public void Refresh() { gen.RegenerateSlots(); }
}
