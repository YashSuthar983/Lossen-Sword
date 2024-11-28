using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SG {
    public class PlayerAttcaker : MonoBehaviour
    {
        PlayerStats playerStats;
        PLayerManager playerManager;
        AnimatorHandler animatorHandler;
        InputHandller inputhandller;
        PlayerInventory playerInventory;
        PlayerEquipmentManager playerEquipmentManager;
        CameraHandler cameraHandler;

        WeaponSlotManager weaponSlotManager;
        public string lastAttck;

        LayerMask backStabLayer=(1<<12);
        LayerMask riposteLayer=(1<<13);

        private void Start()
        {
            cameraHandler=FindObjectOfType<CameraHandler>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerManager =GetComponentInParent<PLayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputhandller=GetComponentInParent<InputHandller>();
            weaponSlotManager=GetComponent<WeaponSlotManager>();
        }


        public  void HandleWeaponCombo(WeaponItem weapon)
        {
            /*
            if(inputhandller.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttck == weapon.Oh_Light_Atk_1)
                {
                    animatorHandler.PlayerTargetAnimation(weapon.Oh_Heavy_Atk_1, true);
                    weaponSlotManager.DrainStamina(false);
                }
                else
                {
                    animatorHandler.PlayerTargetAnimation(weapon.Oh_Light_Atk_1, true);
                    weaponSlotManager.DrainStamina(true);

                }
            }*/

            
        }

        public void HandleLightAttack01(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon=weapon;
            animatorHandler.PlayerTargetAnimation(weapon.Oh_Light_Atk_1, true);
            lastAttck=weapon.Oh_Light_Atk_1;
            weaponSlotManager.DrainStamina(true);
        }
        public void HandleLightAttack02(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayerTargetAnimation(weapon.Oh_Light_Atk_2, true);
            lastAttck = weapon.Oh_Light_Atk_2;
            weaponSlotManager.DrainStamina(true);
        }

        public void HandleHeavyAttack01(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayerTargetAnimation(weapon.Oh_Heavy_Atk_1, true );
            lastAttck=weapon.Oh_Heavy_Atk_1;
            weaponSlotManager.DrainStamina(true);
        }
        public void HandleHeavyAttack02(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayerTargetAnimation(weapon.Oh_Heavy_Atk_2, true );
            lastAttck = weapon.Oh_Heavy_Atk_2;
            weaponSlotManager.DrainStamina(true);
        }

        #region Input Action
        public void Handle_A_InputAction()
        {
            if(playerInventory.handWeapon.isMelee)
            {
                //handle Melle action
                PerformA_Input_MeleeAction();
            }
            else if(playerInventory.handWeapon.isSpellCaster||playerInventory.handWeapon.isFaithCaster||playerInventory.handWeapon.isPyroCaster)
            {
                //handel magic action
                PerformA_Input_MagicAction(playerInventory.handWeapon);
            }
            

            
        }
        public void Handle_LT_InputAction()
        {
            if(playerInventory.handShield.isShield)
            {
                PerformParry(true);
            }
            else 
            {
                PerformParry(false);
            }
        }
        #endregion 

        #region Attack Action
        public void PerformA_Input_MeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputhandller.comboFlag = true;
                HandleWeaponCombo(playerInventory.handWeapon);
                inputhandller.comboFlag = false;
            }
            else
            {
                if (playerManager.isIntarecting)
                {
                    return;
                }
                if (playerManager.canDoCombo)
                    return;

                HandleHeavyAttack02(playerInventory.handWeapon);

            }

        }
        public void PerformA_Input_MagicAction(WeaponItem weapon)
        {
            if (playerManager.isIntarecting)
            { return; }
            
            if (weapon.isFaithCaster)
            {
                if(playerInventory.currentSpell!=null && playerInventory.currentSpell.isFaithSpell)
                {
                    if (playerStats.currentMana >= playerInventory.currentSpell.manaCost)
                    {
                        //check for fp 
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);

                    }
                    else
                    {
                        animatorHandler.PlayerTargetAnimation("Shrug", true);
                    }
                    
                    //cast spell
                }
            }
            else if(weapon.isPyroCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.ispyroSpell)
                {
                    if (playerStats.currentMana >= playerInventory.currentSpell.manaCost)
                    {
                        //check for fp 
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);

                    }
                    else
                    {
                        animatorHandler.PlayerTargetAnimation("Shrug", true);
                    }

                    //cast spell
                }
            }
        }
        public void SuccessFullyCastSpell()
        {
           
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler,playerStats, cameraHandler,
             weaponSlotManager);
        }

        private void PerformParry(bool isShield )
        {
            if(playerManager.isIntarecting) 
                return;

            if(isShield)
            {
                animatorHandler.PlayerTargetAnimation(playerInventory.handShield.parry_with_shield, true);
            }
            else
            {
                animatorHandler.PlayerTargetAnimation(playerInventory.handWeapon.parry_with_weapon, true);
            }
        }

        #endregion
        public void HandleShieldBlock()
        {
            if(playerManager.isIntarecting)
                return ;
            if (playerManager.isBlocking)
            {
                return;
            }

            animatorHandler.PlayerTargetAnimation("Block Idle",false,true);
            playerEquipmentManager.OpenBlockingCollider();
            
            playerManager.isBlocking = true;

        }

        public void AttemptBackStabAndRepost()
        {
            RaycastHit hit;
            if(Physics.Raycast(inputhandller.backstabRayCastPoint.position
                , transform.TransformDirection(Vector3.forward),out hit,0.5f,backStabLayer))
            {
                CharacterManager enemycharacterManager=hit.transform.gameObject.GetComponent<CharacterManager>();
                DamageCollider handWeapon = weaponSlotManager.HandDamageCollider;


                if(enemycharacterManager != null )
                {
                    playerManager.transform.position=enemycharacterManager.backStabCollider.backStabberStandPoint.position;
                    Vector3 rotationDir=playerManager.transform.root.eulerAngles;
                    rotationDir=hit.transform.position  -   playerManager.transform.position;
                    rotationDir.y=0;
                    rotationDir.Normalize();
                    Quaternion tr=Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotaion=Quaternion.Slerp(playerManager.transform.rotation,tr,500*Time.deltaTime);

                    playerManager.transform.rotation=targetRotaion; 

                    int criticalDamage=playerInventory.handWeapon.criticalDamageMultiplier*handWeapon.currentWeaponDamage;
                    enemycharacterManager.pendingCriticalDamage=criticalDamage;

                    animatorHandler.PlayerTargetAnimation("BackStab", true);
                    enemycharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayerTargetAnimation("BackStabbed", true);

                }



            }
            else if (Physics.Raycast(inputhandller.backstabRayCastPoint.position
                , transform.TransformDirection(Vector3.forward), out hit, 1f, riposteLayer))
            {
                CharacterManager enemycharacterManager = hit.transform.gameObject.GetComponent<CharacterManager>();
                DamageCollider handWeapon = weaponSlotManager.HandDamageCollider;


                if (enemycharacterManager != null&&enemycharacterManager.canBeRiposted)
                {
                    playerManager.transform.position = enemycharacterManager.backStabCollider.RiposterStandPoint.position;
                    Vector3 rotationDir = playerManager.transform.root.eulerAngles;
                    rotationDir = hit.transform.position - playerManager.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotaion = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);

                    playerManager.transform.rotation = targetRotaion;

                    int criticalDamage = playerInventory.handWeapon.criticalDamageMultiplier * handWeapon.currentWeaponDamage;
                    enemycharacterManager.pendingCriticalDamage = criticalDamage;

                    animatorHandler.PlayerTargetAnimation("Riposte", true);
                    enemycharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayerTargetAnimation("Riposited", true);

                }



            }
        }
    }
}

