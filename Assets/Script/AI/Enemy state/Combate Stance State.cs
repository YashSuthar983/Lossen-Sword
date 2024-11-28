using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class CombateStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;
        public IdleState idleState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            
            if (enemyManager.isInteracting)
            {
                return this;
            }
            if (enemyManager.currentTarget.isDead)
            {
                return idleState;
            }
            float distanceFromTarget=Vector3.Distance(enemyManager.currentTarget.transform.position,enemyManager.transform.position);
            //check for attack rage

            if(enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical",0,0.1f,Time.deltaTime);
            }

            if(enemyManager.currentRecoveryTime <=0&&distanceFromTarget <=enemyManager.maximumAttackRange)
            {
                return attackState;
            }
            else if(distanceFromTarget > enemyManager.maximumAttackRange)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            /// circle playe 
            /// // out of range 
            /// ]
            /// 
        }
    }
}