using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{
    public class HandEquipmentSlotUI : MonoBehaviour
    {
        public Image icon;
        WeaponItem weaponItem;
        UIManager uiManager;

        public bool HandSlot01;
        public bool HandSlot02;
        public bool HandSlot03;
        public bool HandSlot04;


        private void Awake()
        {
            uiManager=FindAnyObjectByType<UIManager>();
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

        public void SelectThisSlot()
        {
            if(HandSlot01)
            {
                uiManager.HandSlot01_Selected = true;
            }
            else if(HandSlot02)
            {
                uiManager.HandSlot02_Selected = true;
            }
            else if (HandSlot03)
            {
                uiManager.HandSlot03_Selected = true;
            }
            else  
            {
                uiManager.HandSlot04_Selected = true;
            }
        }


    }
}