using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SG
{
    public class PlayerInventory : MonoBehaviour
    {

        WeaponSlotManager weaponSlotManager;

        [Header("Weapon in Right Hand")]
        public WeaponItem handWeapon;
        public WeaponItem[] weaponIn_HandSlots = new WeaponItem[1];

        [Header("Shift/Spell in Left Hand")]
        public SpellItem currentSpell;
        public ShieldItem handShield;

        public Consumables currentCosumable;


        public WeaponItem unArmedWeapon;
        public int current_Weaponindex = -1;
        public List<WeaponItem> weaponInventory;



        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlots(unArmedWeapon,false);
            weaponSlotManager.LoadShieldOnSlots(handShield);
        }

        public void Change_Weapon(bool d_Pad_Left)
        {
            if(d_Pad_Left)
            {
                current_Weaponindex -= 1;

            }
            else
            {
                current_Weaponindex += 1;

            }

            if (current_Weaponindex < -1 )
            {
                current_Weaponindex = weaponIn_HandSlots.Length-1;
                handWeapon = weaponIn_HandSlots[weaponIn_HandSlots.Length - 1];
                weaponSlotManager.LoadWeaponOnSlots(handWeapon, false);

            }



            if (current_Weaponindex == 0 && weaponIn_HandSlots[0]!=null)
            {
                handWeapon = weaponIn_HandSlots[current_Weaponindex];
                weaponSlotManager.LoadWeaponOnSlots(weaponIn_HandSlots[current_Weaponindex],false);
            }

            else if(current_Weaponindex == 0 && weaponIn_HandSlots[0]==null)
            {
                if (d_Pad_Left)
                {
                    current_Weaponindex -= 1;

                }
                else
                {
                    current_Weaponindex += 1;

                }
            }

            else if (current_Weaponindex == 1 && weaponIn_HandSlots[1]!=null)
            {
                handWeapon = weaponIn_HandSlots[current_Weaponindex];
                weaponSlotManager.LoadWeaponOnSlots(weaponIn_HandSlots[current_Weaponindex] ,false);
            }
            else if (current_Weaponindex == 1 && weaponIn_HandSlots[1] == null)
            {
                if (d_Pad_Left)
                {
                    current_Weaponindex -= 1;

                }
                else
                {
                    current_Weaponindex += 1;

                }
            }
            else if (current_Weaponindex == 2 && weaponIn_HandSlots[2] != null)
            {
                handWeapon = weaponIn_HandSlots[current_Weaponindex];
                weaponSlotManager.LoadWeaponOnSlots(weaponIn_HandSlots[current_Weaponindex], false);
            }
            else if (current_Weaponindex == 2 && weaponIn_HandSlots[2] == null)
            {
                if (d_Pad_Left)
                {
                    current_Weaponindex -= 1;

                }
                else
                {
                    current_Weaponindex += 1;

                }
            }
            else if (current_Weaponindex == 3 && weaponIn_HandSlots[3] != null)
            {
                handWeapon = weaponIn_HandSlots[current_Weaponindex];
                weaponSlotManager.LoadWeaponOnSlots(weaponIn_HandSlots[current_Weaponindex], false);
            }
            else if (current_Weaponindex == 3 && weaponIn_HandSlots[3] == null)
            {
                if (d_Pad_Left)
                {
                    current_Weaponindex -= 1;

                }
                else
                {
                    current_Weaponindex += 1;

                }
            }


            else 
            {
                if(d_Pad_Left)
                {
                    current_Weaponindex =- 1;

                }
                else
                {
                    current_Weaponindex += 1;

                }

            }

            

            

            if (current_Weaponindex>weaponIn_HandSlots.Length-1)
            {
                current_Weaponindex =-1;
                

            }
            if(current_Weaponindex==-1)
            {
                handWeapon = unArmedWeapon;
                weaponSlotManager.LoadWeaponOnSlots(handWeapon, false);
            }

        }


       
        
    }
}
