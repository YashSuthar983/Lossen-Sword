using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;
    EnemyStats enemyStats;
    private void Awake()
    {
        anim=GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    public override void TakeCriticalDamageAnimation()
    {
        enemyStats.TakeDamageNoAnim(enemyManager.pendingCriticalDamage);
        enemyManager.pendingCriticalDamage = 0;
    }

    public void EnableIsParring()
    {
        enemyManager.isParring = true;
    }
    public void DisnableIsParring()
    {
        enemyManager.isParring = false;
    }

    public void CanRotate()
    {
        anim.SetBool("canRotate", true);
    }
    public void StopeRotate()
    {
        anim.SetBool("canRotate", false);

    }

    public void EnableCombo()
    {
        anim.SetBool("canDoCombo", true);
    }
    public void DisableCombo()
    {
        anim.SetBool("canDoCombo", false);
    }
    public void EnableCanBeRiposted()
    {
        enemyManager.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        enemyManager.canBeRiposted = true;
    }

    public void AwardEXPonDeath()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        EXPCount ex=FindObjectOfType<EXPCount>();

        if (playerStats != null)
        {
            playerStats.AddEXP(enemyManager.expAwardedOnDeath);

            if (ex != null)
            {
                ex.UpdateExpCount(playerStats.expCount);
            }
        }

        
    }

    private void OnAnimatorMove()
    {
        float delta=Time.deltaTime;
        enemyManager.rb.drag = 0;
        Vector3 deltapos=anim.deltaPosition;
        //deltapos.y=0;
        Vector3 velocity=deltapos/delta;
        enemyManager.rb.velocity = velocity;
    }

}
