using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


namespace SG
{
    public class EnemyStats : CharacterStats
    {


        EnemyManager enemyManager;

        public EnemyHealthBar healthBar;
        Animator animator;

        void Start()
        {
            enemyManager = GetComponent<EnemyManager>();    
            animator = GetComponentInChildren<Animator>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private float SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamageNoAnim(float damage)
        {
            if (isDead) return;

            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);
            if(currentHealth<= 0)
            {
                isDead = true;
                currentHealth= 0;   
            }
        }

        public override void TakeDamage(float damage,string damageAnime="Mutant_Rib_Hit")

        {
            if(isDead) return;

            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);
            damageAnime = "Mutant_Rib_Hit";

            animator.Play(damageAnime);
            if (currentHealth <= 0)
            {
                HandleDeath();
                
            }



        }


        public void HandleDeath()
        {
            animator.Play("Mutant_dying");
            isDead = true;
            enemyManager.currentState = enemyManager.deadState;

            
        }
    }

}
