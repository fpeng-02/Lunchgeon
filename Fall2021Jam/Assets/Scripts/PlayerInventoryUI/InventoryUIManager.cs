using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    private bool inventoryIsOpen;
    private InventorySlotsManager ism;
    [SerializeField] private ActiveItemUI activeItem;
    [SerializeField] private GameObject backpackGO;
    [SerializeField] private GameObject activeItemGO;

    void Start()
    {
        ism = GetComponentInChildren<InventorySlotsManager>();
        activeItem = GetComponentInChildren<ActiveItemUI>();
        //activeItemGO.SetActive(false);
        backpackGO.SetActive(false);
    }

    public void ToggleBackpackEnable() 
    {
        if (inventoryIsOpen) {
            backpackGO.SetActive(false);
            inventoryIsOpen = false;
        } else {
            ism.RegenerateSlots();
            backpackGO.SetActive(true);
            inventoryIsOpen = true;
        }
    }
    public void Refresh() { ism.RegenerateSlots(); }

    void Update()
    {
        if (!inventoryIsOpen) return;
        if (Input.GetButtonDown("EquipItem"))
        {
            ism.EquipItemData();
            //activeItemGO.SetActive(true);
            activeItem.UpdateActiveItemUI();
        }
        else if (Input.GetButtonDown("DropItem"))
        {
            ism.DropSelectedItem();
        }
    }
}
