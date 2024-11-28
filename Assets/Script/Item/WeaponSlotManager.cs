using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class WeaponSlotManager : MonoBehaviour
    {
        
        public  WeaponHolderSlots rightHandSlot;
        public WeaponHolderSlots leftHandSlot;


        public DamageCollider HandDamageCollider;

        Animator animator;
        QuickSlotUI quickSlotUI;
        PlayerStats playerStats;
        InputHandller inputHandller;
        PLayerManager playerManager;
        PlayerInventory playerInventory;

        public WeaponItem attackingWeapon;



        private void Awake()
        {
            inputHandller=GetComponentInParent<InputHandller>();
            playerInventory=GetComponentInParent<PlayerInventory>();
            animator = GetComponent<Animator>();
            quickSlotUI=FindObjectOfType<QuickSlotUI>();
            playerStats=GetComponentInParent<PlayerStats>();
            playerManager =FindAnyObjectByType<PLayerManager>();

            WeaponHolderSlots[]weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlots>();

            foreach(WeaponHolderSlots weaponSlot in weaponHolderSlots)
            {
                rightHandSlot = weaponSlot;
            }


        }


        public void LoadBothItemsOnHand()
        {
            LoadWeaponOnSlots(playerInventory.handWeapon, false);
            LoadShieldOnSlots(playerInventory.handShield);
        }

        public void LoadWeaponOnSlots(WeaponItem weaponItem,bool isLeft)
        {

            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadWeaponDamageCollider();
            quickSlotUI.UpdateQuickSlotIcon(false, weaponItem);
            #region handle  weapon idle anime
            if (weaponItem != null)
            {
                
                animator.CrossFade(weaponItem.weapon_Hand_idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Empty", 0.2f);
            }
            #endregion

        }

        public void LoadShieldOnSlots(ShieldItem shieldItem)
        {

            leftHandSlot.LoadShieldModel(shieldItem);
            
            #region handle  weapon idle anime
            if (shieldItem != null)
            {

                animator.CrossFade(shieldItem.shield_Hand_idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Empty", 0.2f);
            }
            #endregion

        }


        #region Handle Wepon Damage Collider


        private void LoadWeaponDamageCollider()
        {
            HandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            HandDamageCollider.currentWeaponDamage = playerInventory.handWeapon.baseDamage;
        }

        public void OpenHandDamageCollider()
        {
            HandDamageCollider.EnableDamageCollider();
        }

        public void CloseHandDamageCollider()
        {
            HandDamageCollider.DisableDamageCollider();

        }


        #endregion

        public void DrainStamina(bool islight)
        {
            if (islight)
            {
                playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lighAttackMultiplier));
            }
            else
            {
                playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));

            }
        }

    }
}