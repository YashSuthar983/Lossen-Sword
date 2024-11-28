using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        
        Collider damageCollider;
        public int currentWeaponDamage = 25;

        public bool enableDamageColliderOnStartUp=false;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger=true;
            damageCollider.enabled=enableDamageColliderOnStartUp;
            
        }
        public void EnableDamageCollider()
        {
            damageCollider.enabled=true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled=false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag=="Player")
            {
                PlayerStats playerStats=other.GetComponentInParent<PlayerStats>();
                CharacterManager enemyCharacterManager=other.GetComponentInParent<CharacterManager>();   
                BlockingCollider shield=other.GetComponentInParent<BlockingCollider>();
                
                if(enemyCharacterManager!=null)
                {
                    if(enemyCharacterManager.isParring)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayerTargetAnimation("Parried", true);
                        return;
                    }
                    else if(shield!=null&&enemyCharacterManager.isBlocking)
                    {
                        print("ready to damge");

                        float physicalDamageAfterBlock = (currentWeaponDamage - currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;
                        if(playerStats!=null)
                        {
                            print("damge");
                            playerStats.TakeDamage(physicalDamageAfterBlock,"Block Guard");
                            return;
                        }
                    }
                }
                
                if(playerStats!=null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }
            if (other.tag == "Enemy")
            {
                EnemyStats enemyStats = other.GetComponentInParent<EnemyStats>();
                CharacterManager enemyCharacterManager = other.GetComponentInParent<CharacterManager>();
                BlockingCollider shield = other.GetComponentInParent<BlockingCollider>();

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.isParring)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayerTargetAnimation("Parried", true);
                        return ;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking)
                    {
                        float physicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;
                        if (enemyStats != null)
                        {
                            enemyStats.TakeDamage(physicalDamageAfterBlock);
                            return;
                        }
                    }
                }

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }

        }
    }

}