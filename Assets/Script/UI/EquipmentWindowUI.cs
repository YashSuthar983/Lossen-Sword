using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool HandSlot01Selected;
        public bool HandSlot02Selected;
        public bool HandSlot03Selected;
        public bool HandSlot04Selected;

        public Transform handSlotsParent;

        public HandEquipmentSlotUI[] handEquipmentSlotUI;

       

        public void LoadWeaponOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for (int i = 0; i < handEquipmentSlotUI.Length; i++)
            {
                if (handEquipmentSlotUI[i].HandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponIn_HandSlots[0]);

                }
                else if (handEquipmentSlotUI[i].HandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponIn_HandSlots[1]);

                }
                else if (handEquipmentSlotUI[i].HandSlot03)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponIn_HandSlots[2]);

                }
                else  
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponIn_HandSlots[3]);

                }
            }
        }

        public void SelectHandSlot01()
        {
            HandSlot01Selected = true;
        }
        public void SelectHandSlot02()
        {
            HandSlot02Selected = true;
        }
        public void SelectHandSlot03()
        {
            HandSlot03Selected=true;
        }
        public void SelectHandSlot04()
        {
            HandSlot04Selected=true;    
        }
    }
}
