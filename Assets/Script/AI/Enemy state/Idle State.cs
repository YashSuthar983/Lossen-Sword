
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;

        public LayerMask detectionLayer;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //look for target
            // switch to pusue target

            

            if(enemyManager.canBackStab)
            {
                
            }


            #region Handle enemy arget Detection 
            Collider[] collider = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < collider.Length; i++)
            {
                CharacterStats characterStats = collider[i].transform.GetComponentInParent<CharacterStats>();

                if (characterStats != null)
                {
                    if (characterStats.isDead)
                    { continue; }
                    Vector3 targetDir = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDir, transform.forward);


                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                    }
                }

            }
            #endregion


            #region Handle Switch State
            if (enemyManager.currentTarget != null)
            {
                return pursueTargetState;

            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}