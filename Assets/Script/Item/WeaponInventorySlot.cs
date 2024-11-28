using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        public Image icon;
        WeaponItem weaponItem;
        UIManager uiManager;
        PlayerInventory playerInventory;
        WeaponSlotManager weaponSlotManager;
        EquipmentWindowUI equipmentWindowUI;
        private void Awake()
        {
            uiManager=FindAnyObjectByType<UIManager>();
            playerInventory = FindAnyObjectByType<PlayerInventory>();   
            weaponSlotManager = FindAnyObjectByType<WeaponSlotManager>();
            equipmentWindowUI = FindAnyObjectByType<EquipmentWindowUI>();
        }

        public void AddItem(WeaponItem newWeaponItem)
        {
            weaponItem = newWeaponItem;
            icon.sprite=weaponItem.itemIcon;
            icon.enabled= true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            weaponItem=null;
            icon.sprite=null;
            icon.enabled= false;
            gameObject.SetActive(false);

        }

        public void EquipThisItem()
        {
            if (uiManager.HandSlot01_Selected)
            {
                playerInventory.weaponInventory.Add(playerInventory.weaponIn_HandSlots[0]);
                playerInventory.weaponIn_HandSlots[0]= weaponItem;
                playerInventory.weaponInventory.Remove(weaponItem);
            }
            else if (uiManager.HandSlot02_Selected)
            {
                playerInventory.weaponInventory.Add(playerInventory.weaponIn_HandSlots[1]);
                playerInventory.weaponIn_HandSlots[1] = weaponItem;
                playerInventory.weaponInventory.Remove(weaponItem);

            }
            else if (uiManager.HandSlot03_Selected)
            {
                playerInventory.weaponInventory.Add(playerInventory.weaponIn_HandSlots[0]);
                playerInventory.weaponIn_HandSlots[2] = weaponItem;
                playerInventory.weaponInventory.Remove(weaponItem);

            }
            else if(uiManager.HandSlot04_Selected)
            {
                playerInventory.weaponInventory.Add(playerInventory.weaponIn_HandSlots[1]);
                playerInventory.weaponIn_HandSlots[3] = weaponItem;
                playerInventory.weaponInventory.Remove(weaponItem);

            }
            else
            {
                return;
            }
            playerInventory.handWeapon = playerInventory.weaponIn_HandSlots[playerInventory.current_Weaponindex];


            weaponSlotManager.LoadWeaponOnSlots(playerInventory.handWeapon, false);

            equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventory);
            uiManager.ResetAllSelectedSlots();
        }


    }
}