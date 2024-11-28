using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SG
{
    public class AttackState : State
    {
        public CombateStanceState combateStanceState;
        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttacks;

        bool willDoComboOnNextAttack=false;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if(enemyManager.isInteracting&& enemyManager.canDoCombo==false)
            {
                return this;
            }
            else if(enemyManager.isInteracting&&enemyManager.canDoCombo)
            {
                if(willDoComboOnNextAttack)
                {
                    enemyAnimatorManager.PlayerTargetAnimation(currentAttacks.actionAnimation,true);
                    willDoComboOnNextAttack = false;
                }
            }

            Vector3 targdetDir = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targdetDir, transform.forward);
            float distanceFromTarget=Vector3.Distance(enemyManager.currentTarget.transform.position,enemyManager.transform.position);

            if (enemyManager.isPerformingAction)
                return combateStanceState;

            if(currentAttacks!=null)
            {
                if (distanceFromTarget < currentAttacks.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                else if (distanceFromTarget < currentAttacks.maximumDistanceNeededToAttack)
                {
                    if(viewableAngle <=currentAttacks.maximumAttackAngle&& 
                        viewableAngle >= currentAttacks.minimumAttackAngle)
                    {
                        if(enemyManager.currentRecoveryTime<=0&& enemyManager.isPerformingAction==false)
                        {
                            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.anim.SetFloat("Horizontal",0,0.1f,Time.deltaTime);
                            enemyAnimatorManager.PlayerTargetAnimation(currentAttacks.actionAnimation,true);
                            enemyManager.isPerformingAction=true;

                            if(currentAttacks.canCombo)
                            {
                                currentAttacks=currentAttacks.combooAction;
                                return this;
                            }
                            else
                            {
                                enemyManager.currentRecoveryTime = currentAttacks.recoveryTime;
                                currentAttacks = null;
                                return combateStanceState;

                            }

                        }
                    }

                }
                
            }
            else
            {
                GetNewAttack(enemyManager);
            }

            return combateStanceState;
        }

        #region Attack


        

        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
            float viewableangle = Vector3.Angle(targetDir, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableangle <= enemyAttackAction.maximumAttackAngle
                        && viewableangle >= enemyAttackAction.minimumAttackAngle)
                    {

                        maxScore += enemyAttackAction.attackscore;
                    }


                }




            }

            int randomValue = Random.Range(0, maxScore);
            int tempScore = 0;

            for (int j = 0; j < enemyAttacks.Length; j++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[j];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableangle <= enemyAttackAction.maximumAttackAngle
                        && viewableangle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttacks != null)
                        {
                            return;
                        }

                        tempScore += enemyAttackAction.attackscore;

                        if (tempScore > randomValue)
                        {
                            currentAttacks = enemyAttackAction;
                        }


                    }


                }




            }


        }

        #endregion
    }

}
