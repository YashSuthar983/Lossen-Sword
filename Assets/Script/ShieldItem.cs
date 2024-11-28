using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{

    [CreateAssetMenu(menuName =("Items/Sheild Item"))]
    public class ShieldItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;


        [Header("Absorption")]
        public float physicalDamageAbsorption;

        [Header("Idle Anime")]
        public string shield_Hand_idle;

        [Header("Parry Anim")]
        public string parry_with_shield;


        [Header("Item type")]
        public bool isShield;
        public bool isTalisman;
    }
}