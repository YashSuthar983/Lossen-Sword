using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SG
{
    public class PlayerEffectsManager : MonoBehaviour
    {
        public GameObject currentParticleFX;
        public GameObject instantiateFXModel;


        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;


        public float healRecoveryAmount;
        public float manaRecoveryAmount;
        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HealPlayerFromEffects()
        {
            
            playerStats.HealPlayer(healRecoveryAmount);
            GameObject healFX=Instantiate(currentParticleFX,playerStats.transform); 
        }

        public void ManaRecoveryFromEffects()
        {
            playerStats.ManaRecovery(manaRecoveryAmount);
            GameObject manaFX = Instantiate(currentParticleFX, playerStats.transform);
        }

        public void DestroyModelWhenAnimatonComplets()
        {
            Destroy(instantiateFXModel.gameObject);
            weaponSlotManager.LoadBothItemsOnHand();

        }
    }
}
