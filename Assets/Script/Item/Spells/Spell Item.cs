using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnim;

        public float manaCost;

        [Header("Spell type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool ispyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;


        public virtual void AttemptToCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            Debug.Log("cast spell attempt");
        }



        public virtual void SuccessfullyCastSpell(AnimatorHandler animatorHandler, 
            PlayerStats playerStats,
            CameraHandler cameraHandler,
            WeaponSlotManager weaponSlotManager)
        {
            playerStats.DrainMana(manaCost);
            Debug.Log(" spell casted");
        }
    }
}