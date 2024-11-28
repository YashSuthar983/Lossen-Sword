using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class Ambushstate : State
    {
        public bool isSleeping;
        public float detectionRadius = 2;
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayer;

        public string sleepAnimation;
        public string wakeupAnimation;


        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if(isSleeping && enemyManager.isInteracting==false)
            {
                enemyAnimatorManager.PlayerTargetAnimation(sleepAnimation, true);
            }

            Collider[] collider = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < collider.Length; i++)
            {
                CharacterStats characterStats = collider[i].transform.GetComponentInParent<CharacterStats>();

                if (characterStats != null)
                {

                    Vector3 targetDir = characterStats.transform.position -enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetDir,enemyManager.transform.forward);


                    if (viewableAngle > enemyManager.minimumDetectionAngle &&
                        viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        isSleeping = false;
                        enemyAnimatorManager.PlayerTargetAnimation(wakeupAnimation, true);

                    }
                }

            }

            if(enemyManager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
        }
    }
}