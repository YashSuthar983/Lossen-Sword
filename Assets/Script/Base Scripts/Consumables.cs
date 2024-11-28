using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class Consumables : Item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("animations")]
        public string consumeAnimation;
        public bool isInteracting;


        public virtual void AttemptToConsumItem(AnimatorHandler playerAnimatorHadler,WeaponSlotManager weaponSlotManager,PlayerEffectsManager playerEffectsManager)
        {
            if(currentItemAmount>0)
            {
                playerAnimatorHadler.PlayerTargetAnimation(consumeAnimation, isInteracting,true);
            }
            else
            {
                playerAnimatorHadler.PlayerTargetAnimation("Shrug", true);
            }    
        }
    }
}
