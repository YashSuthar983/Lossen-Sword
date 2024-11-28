using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu(menuName =("Items/Cosumables/Flasks"))]
public class FlaskItem : Consumables
{
    [Header("Flask Type")]
    public bool healFlask;
    public bool manaFlask;
    public bool mana_healFlask;

    [Header("Recovery Amount")]
    public float healRecoverAmount;
    public float manaRecoverAmount;


    [Header("Recovery Fx")]
    public GameObject recoverryFX;

    public override void AttemptToConsumItem(AnimatorHandler playerAnimatorHadler, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        if(healFlask)
        {
            base.AttemptToConsumItem(playerAnimatorHadler, weaponSlotManager, playerEffectsManager);
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.currentParticleFX = recoverryFX;
            playerEffectsManager.healRecoveryAmount = healRecoverAmount;


            playerEffectsManager.instantiateFXModel = flask;
            weaponSlotManager.rightHandSlot.UnloadWeapon();
        }
        if(manaFlask)
        {
            base.AttemptToConsumItem(playerAnimatorHadler, weaponSlotManager, playerEffectsManager);
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.currentParticleFX = recoverryFX;
            playerEffectsManager.manaRecoveryAmount = manaRecoverAmount;


            playerEffectsManager.instantiateFXModel = flask;
            weaponSlotManager.rightHandSlot.UnloadWeapon();
        }
        if(mana_healFlask)
        {
            base.AttemptToConsumItem(playerAnimatorHadler, weaponSlotManager, playerEffectsManager);
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.currentParticleFX = recoverryFX;
            playerEffectsManager.healRecoveryAmount = healRecoverAmount;
            playerEffectsManager.manaRecoveryAmount = manaRecoverAmount;


            playerEffectsManager.instantiateFXModel = flask;
            weaponSlotManager.rightHandSlot.UnloadWeapon();
        }

        
        //insttatiate model i had
        //play aimation

    }


}
