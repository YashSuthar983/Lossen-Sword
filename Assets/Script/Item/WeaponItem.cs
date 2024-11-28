using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    [CreateAssetMenu(menuName ="Items/Weapon Item")]

    public class WeaponItem : Item
    {

        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public int baseDamage=25;
        public int criticalDamageMultiplier=4;
        


        [Header("Idle Anime")]
        public string weapon_Hand_idle;

        [Header("IParry Anime")]
        public string parry_with_weapon;



        [Header("one handed attacks")]
        public string Oh_Light_Atk_1;
        public string Oh_Light_Atk_2;
        public string Oh_Heavy_Atk_1;
        public string Oh_Heavy_Atk_2;



        [Header("Stamia Cost")]
        public int baseStamina;
        public float lighAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Item type")]
        public bool isMelee;
        public bool isSpellCaster;
        public bool isPyroCaster;
        public bool isFaithCaster;
        

    }
}

