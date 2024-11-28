using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public EquipmentWindowUI equipmentWindowUI;
    
    

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotParent;
    WeaponInventorySlot[] weaponInventorySlots;


    [Header("Equipment slot selected bools")]
    public bool HandSlot01_Selected;
    public bool HandSlot02_Selected;
    public bool HandSlot03_Selected;
    public bool HandSlot04_Selected;



    private void Start()
    {
        equipmentWindowUI=FindAnyObjectByType<EquipmentWindowUI>();
        weaponInventorySlots= weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventory);
    }

    public void UpdateInventoryUI()
    {
        #region Weapon Iventory Slots

        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if(i<playerInventory.weaponInventory.Count)
            {
                if(weaponInventorySlots.Length<playerInventory.weaponInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                    weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>(true);
                }
                weaponInventorySlots[i].AddItem(playerInventory.weaponInventory[i]);    
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }

        #endregion
    }

    public void ResetAllSelectedSlots()
    {
        HandSlot01_Selected = false;
        HandSlot02_Selected = false;
        HandSlot03_Selected = false;
        HandSlot04_Selected = false;   
    }
}
