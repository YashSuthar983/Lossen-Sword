using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponPickUp : Interactables
    {
        public WeaponItem weapon;



        public override void Interact(PLayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);

        }

        private void PickUpItem(PLayerManager playerManager)

        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren <AnimatorHandler>();


            playerLocomotion.rb.velocity = Vector3.zero;//stop player
            animatorHandler.PlayerTargetAnimation("Pick Up Item",true);//play pick up anim
            playerInventory.weaponInventory.Add(weapon);
            playerManager.itemInteractableNameUIGameOBJ.GetComponentInChildren<Text>().text =weapon.itemName;
            playerManager.itemInteractableIcon.GetComponentInChildren<RawImage>().texture=weapon.itemIcon.texture;
            playerManager.itemInteractableIcon.SetActive(true);
            playerManager.itemInteractableNameUIGameOBJ.SetActive(true) ;
            
            Destroy(gameObject);


        }
    }
}
