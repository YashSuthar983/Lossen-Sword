using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class OpenChest : Interactables
    {
        public Transform playerStandingPos;
        Animator animator;
        OpenChest openChest;

        public GameObject itemSpawner;
        public WeaponItem itemInChest;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            openChest = GetComponent<OpenChest>();
        }

        public override void Interact(PLayerManager playerManager)
        {
            //rotate player toward chest 

            Vector3 rotationDir=transform.position-playerManager.transform.position;
            rotationDir.y=0;
            rotationDir.Normalize();

            Quaternion tr=Quaternion.LookRotation(rotationDir);
            Quaternion targetrotation=Quaternion.Slerp(playerManager.transform.rotation, tr,200*Time.deltaTime);

            playerManager.transform.rotation=targetrotation;

            //lock transform

            playerManager.OpenChestinteraction(playerStandingPos);


            //open chest lid

            animator.Play("Chest Open");

            //spawn item inside chest
            StartCoroutine(SpawnItemInChest());

            WeaponPickUp weaponPickUp=itemSpawner.GetComponent<WeaponPickUp>();
            if(weaponPickUp!=null)
            {
                weaponPickUp.weapon = itemInChest;
            }


        }

        private IEnumerator SpawnItemInChest()
        {
            yield return new WaitForSeconds(1);
            Instantiate(itemSpawner, transform);
            Destroy(openChest);
        }
    }
}