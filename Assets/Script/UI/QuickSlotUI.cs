using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class QuickSlotUI : MonoBehaviour
    {
        public Image leftSlotIcon;
        public Image rightSlotIcon;




        public void UpdateQuickSlotIcon(bool isLeft,WeaponItem weapon)
        {
            if ((isLeft))
            {
                if(weapon.itemIcon!=null)
                {
                    leftSlotIcon.sprite=weapon.itemIcon;
                    leftSlotIcon.enabled=true;

                }
                else
                {
                    leftSlotIcon.sprite=null;   
                    leftSlotIcon.enabled=false;
                }
            }
            else
            {
                if (weapon.itemIcon != null)
                {
                    rightSlotIcon.sprite = weapon.itemIcon;
                    rightSlotIcon.enabled = true;

                }
                else
                {
                    rightSlotIcon.sprite = null;
                    rightSlotIcon.enabled = false;
                }
            }
        }
    }
}
