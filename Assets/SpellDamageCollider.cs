using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SpellDamageCollider : DamageCollider
    {
        public GameObject impactParticle;
        public GameObject projectileParticle;
        public GameObject muzzleParticle;

        bool hasCollide=false;
        Vector3 impactNormal;

        CharacterStats spellTarget;

        private void Start()
        {
            projectileParticle=Instantiate(projectileParticle,transform.position,transform.rotation) ;
            projectileParticle.transform.parent = transform ;
            if(muzzleParticle)
            {
                muzzleParticle=Instantiate(muzzleParticle,transform.position,transform.rotation) ;
                Destroy(muzzleParticle,2f);
            }

        }

        //private void OnCollisionEnter(Collision other)
        //{
        //    Debug.Log("collided");
        //    if (!hasCollide)
        //    {

        //        spellTarget = other.transform.GetComponent<CharacterStats>();
        //        Debug.Log("object found");
        //        if (spellTarget != null)
        //        {
        //            Debug.Log("spell damage enemy");
        //            spellTarget.TakeDamage(currentWeaponDamage);

        //        }

        //        hasCollide = true;
        //        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                
        //        Destroy(projectileParticle);
        //        Destroy(impactParticle, 1.5f);
        //        Destroy(gameObject, 3);
        //    }
        //}

        private void OnTriggerEnter(Collider other)
        {
            
            if (!hasCollide)
            {
                spellTarget = other.transform.GetComponent<CharacterStats>();

                if (spellTarget != null)
                {
                    Debug.Log("spell damage enemy");

                    spellTarget.TakeDamage(currentWeaponDamage);

                }

                hasCollide = true;
                impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectileParticle);
                Destroy(impactParticle, 1.5f);
                Destroy(gameObject, 3);
            }
        }


    }
}