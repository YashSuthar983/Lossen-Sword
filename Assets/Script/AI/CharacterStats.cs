using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public float maxHealth;
        public float currentHealth;

        public float currentStamina;
        public float maxStamina;
        public int staminaLevel=10;
        public float staminaregenrationPower;

        public float currentMana;
        public float maxMana;
        public float manaLevel=10;
        public float manaRegenrationPower;


        public int expCount=0;

        public bool isDead;

        public virtual void TakeDamage(float damage, string damageAnim = "Damage_01")
        {

        }

    }
}