using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;

namespace SG
{
    public class InputHandller : MonoBehaviour
    {
        public float Horizontal;
        public float Vertical;
        public float MoveAmount;
        public float MouseX;
        public float MouseY;



        public bool jump_Input;
        public bool lockOn_input;
        public bool roll_Input;
        public bool f_Input;
        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;
        public bool lockOnRight_Input;
        public bool lockOnLeft_Input;
        public bool x_Input;
        public bool y_Input;
        public bool a_Input;
        public bool b_Input;
        public bool backStab_Input;
        public bool parry_Input;
        public bool shield_Input;
        public bool consumeable_Input;
        public bool healRecovery_Input;
        public bool manaRecovery_Input;



        public bool rollFlag;
        public float rollInputTimer;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;



        public Transform backstabRayCastPoint;
        PlayerControls InputAction;
        PlayerAttcaker playerAttcaker;
        PlayerInventory playerInventory;
        PLayerManager playerManager;
        CameraHandler cameraHandler;
        BlockingCollider blockingCollider;
        AnimatorHandler animatorHandler;
        PlayerEffectsManager playerEffectsManager;
        WeaponSlotManager weaponSlotManager;
        PlayerStats playerStats;



        Vector2 MovementInput;
        Vector2 CameraInput;



        private void Awake()
        {
            playerAttcaker = GetComponentInChildren<PlayerAttcaker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager=GetComponent<PLayerManager>();
            cameraHandler=FindAnyObjectByType<CameraHandler>(); 
            blockingCollider=GetComponentInChildren<BlockingCollider>();
            animatorHandler=GetComponentInChildren<AnimatorHandler>();
            playerEffectsManager=GetComponentInChildren<PlayerEffectsManager>();
            weaponSlotManager=GetComponentInChildren<WeaponSlotManager>();
           playerStats=GetComponent<PlayerStats>();   
        }

        public void OnEnable()
        {
            if (InputAction == null)
            {
                InputAction = new PlayerControls();
                InputAction.PlayerMovement.Movment.performed += InputAction => MovementInput= InputAction.ReadValue<Vector2>();
                InputAction.PlayerMovement.Camera.performed += i => CameraInput= i.ReadValue<Vector2>();
                InputAction.PlayerQuickSlots.D_Pad_Right.performed += i => d_Pad_Right = true;
                InputAction.PlayerQuickSlots.D_Pad_Left.performed += i => d_Pad_Left = true;
                InputAction.PlayerAction.Interactables.performed += i => f_Input = true;
                InputAction.PlayerMovement.Jump.performed += i => jump_Input = true;
                InputAction.PlayerAction.LockOn.performed += i => lockOn_input = true;
                InputAction.PlayerAction.LockOnTargetLeft.performed += i => lockOnLeft_Input = true;
                InputAction.PlayerAction.LockOnTargetRight.performed += i => lockOnRight_Input = true;
                InputAction.PlayerAction.A.performed += i => a_Input = true;
                InputAction.PlayerAction.B.performed += i => b_Input = true;
                InputAction.PlayerAction.X.performed += i => x_Input = true;
                InputAction.PlayerAction.Y.performed += i => y_Input = true;
                InputAction.PlayerAction.BackStab.performed += i => backStab_Input = true;
                InputAction.PlayerAction.Parry.performed += i => parry_Input = true;
                InputAction.PlayerAction.Shield.performed += i => shield_Input = true;
                InputAction.PlayerAction.Shield.canceled += i => shield_Input = false;
                InputAction.PlayerAction.Consumable.performed += i => consumeable_Input = true;
                InputAction.PlayerAction.HealRecovery.performed += i => healRecovery_Input = true;
                InputAction.PlayerAction.ManaREcovery.performed += i => manaRecovery_Input   = true;


            }
            InputAction.Enable();
        }


        private void OnDisable()
        {
            InputAction.Disable();
        }

        public void TickINPUT(float delta)
        {
            HnadleMoveInput(delta);
            HandleRollInput(delta);
            HandleCombatInput(delta);
            HandleQuickSlotInput();
            HandleLockOnIput();
            HandleBackStabInput();
            HandleHealRecoveryInput();
            HandleManaRecoveryInput();

        }
        private void HnadleMoveInput(float delta) 
        {
            

            Horizontal = MovementInput.x;
            Vertical = MovementInput.y;

            MoveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
            MouseX= CameraInput.x;
            MouseY= CameraInput.y;  
        }
        private void HandleRollInput(float delta)
        {
            //b_Input = InputAction.PlayerAction.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //b_Input = InputAction.PlayerAction.Roll.triggered;
            roll_Input = InputAction.PlayerAction.Roll.IsPressed();
            sprintFlag = roll_Input;
            if (roll_Input)
            {
                rollInputTimer += delta;

                


            }
            else
            {
                if(rollInputTimer>0&&rollInputTimer<0.2f)
                {
                    sprintFlag= false;
                    rollFlag=true;
                }
                rollInputTimer= 0;
            }
            
        }
        private void HandleCombatInput(float delta)
        {
            if(shield_Input)
            {
                playerAttcaker.HandleShieldBlock();
            }
            else
            {
                playerManager.isBlocking= false;
                if(blockingCollider.blockingColliders.enabled)
                {
                    blockingCollider.DisableBlockingCollider();
                }
            }

            if(parry_Input)
            {
                playerAttcaker.Handle_LT_InputAction();
            }

            if (x_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttcaker.HandleWeaponCombo(playerInventory.handWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isIntarecting)
                    {
                        return;
                    }
                    if (playerManager.canDoCombo)
                        return;

                    playerAttcaker.HandleHeavyAttack01(playerInventory.handWeapon);
                }
            }
            if (y_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttcaker.HandleWeaponCombo(playerInventory.handWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isIntarecting)
                    {
                        return;
                    }
                    if (playerManager.canDoCombo)
                        return;
                    playerAttcaker.HandleLightAttack01(playerInventory.handWeapon);
                }
            }
            if (a_Input)
            {
                playerAttcaker.Handle_A_InputAction();
            }
            if (b_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttcaker.HandleWeaponCombo(playerInventory.handWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isIntarecting)
                    {
                        return;
                    }
                    if (playerManager.canDoCombo)
                        return;
                    playerAttcaker.HandleLightAttack02(playerInventory.handWeapon);
                }
            }
        }
        private void HandleQuickSlotInput()
        {
            if (d_Pad_Right)
            {
                playerInventory.Change_Weapon(false);
            }
            else if(d_Pad_Left)
            {
                playerInventory.Change_Weapon(true);
            }
        }
        private void HandleLockOnIput()
        {
            if (lockOn_input && lockOnFlag == false)
            {
                lockOn_input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOntarget!=null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOntarget;
                    lockOnFlag = true;

                }
            }
            else if(lockOn_input && lockOnFlag)
            {
                lockOnFlag = false;
                lockOn_input = false;
                cameraHandler.ClearLockOntarget();

            }
            if (lockOnLeft_Input && lockOnFlag)
            {
                lockOnLeft_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget!=null)
                {
                    cameraHandler.currentLockOnTarget=cameraHandler.leftLockTarget;
                }
            }
            if (lockOnRight_Input && lockOnFlag)
            {
                lockOnRight_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();

        }
        private void HandleBackStabInput()
        {
            if(backStab_Input)
            {
                backStab_Input = false;
                playerAttcaker.AttemptBackStabAndRepost();
            }
        }
        private void HandleConsumeableInput()
        {
            if (consumeable_Input)
            {
                consumeable_Input = false;

            }
        }

        private void HandleHealRecoveryInput()
        {
            if(healRecovery_Input && playerStats.isDead==false)
            {
                healRecovery_Input = false;
                playerInventory.currentCosumable.AttemptToConsumItem(animatorHandler, weaponSlotManager, playerEffectsManager);
            }
        }

        private void HandleManaRecoveryInput()
        {
            if(manaRecovery_Input && playerStats.isDead == false)
            {
                manaRecovery_Input = false;
                playerInventory.currentCosumable.AttemptToConsumItem(animatorHandler, weaponSlotManager, playerEffectsManager);

            }
        }


    }
}

