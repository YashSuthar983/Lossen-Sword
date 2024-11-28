using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SG {


    [CreateAssetMenu(menuName ="Spells/Projectile Spell")]

    public class ProjectileSpell : SpellItem
    {
        public float baseDamage;
        public float projectileForwardVelocity;
        public float projectileUpwardVelocity;
        public float projectileMass;
        public bool isAffectedByGravity;
        Rigidbody rb;

        public override void AttemptToCastSpell(AnimatorHandler animatorHandler, 
            PlayerStats playerStats,
            WeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);

            //instantiate fx

            GameObject instantiateWarmUpFX = Instantiate(spellWarmUpFX, weaponSlotManager.rightHandSlot.transform);
            animatorHandler.PlayerTargetAnimation(spellAnim,true);


            //play animation to cast


        }

        public override void SuccessfullyCastSpell(AnimatorHandler animatorHandler, 
            PlayerStats playerStats,
            CameraHandler cameraHandler,
            WeaponSlotManager weaponSlotManager)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats,cameraHandler,weaponSlotManager);

            GameObject instantiateSpellFx = Instantiate(spellCastFX, weaponSlotManager.rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
            rb=instantiateSpellFx.GetComponent<Rigidbody>();

            //spell damage collider=instaantiate.getcomponet<spelldasd

            if(cameraHandler.currentLockOnTarget!=null)
            {
                instantiateSpellFx.transform.LookAt(cameraHandler.currentLockOnTarget.transform);   
            }
            else
            {
                instantiateSpellFx.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);
            }


            Debug.Log("successfully casted");
            rb.AddForce(instantiateSpellFx.transform.forward * projectileForwardVelocity);
            rb.AddForce(instantiateSpellFx.transform.up * projectileUpwardVelocity);
            rb.useGravity = isAffectedByGravity;
            rb.mass=projectileMass;
            instantiateSpellFx.transform.parent=null;
        
        
        
        }
    }
}