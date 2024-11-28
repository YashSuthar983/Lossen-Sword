
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class PlayerStats : CharacterStats
    {

        PLayerManager playerManger;

        public PlayerHealthBar healthBar;
        public PlayerStaminaBar staminaBar;
        public PlayerManaBar manaBar;
        AnimatorHandler animatorHandler;


        float staminaRegenTimer;
        float manaRegenTimer;

        void Start ()
        {
            playerManger=GetComponent<PLayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            maxHealth=SetMaxHealthFromHealthLevel();
            currentHealth=maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxHealthFromHealthLevel();
            currentStamina=maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);

            maxMana=SetManaFromMaxManaLevel();
            currentMana=maxMana;
            manaBar.SetMaxMana(maxMana);
            manaBar.SetCurrentMana(currentMana);
        }



        #region Health Related
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
            if (currentHealth <= 0)
            {
                isDead = true;
                animatorHandler.PlayerTargetAnimation("Dying_01", true);

                currentHealth = 0;
            }
        }

        public override void TakeDamage(float damage,string damageAnim="Damage_01")
        {
            if (isDead) return;
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                animatorHandler.PlayerTargetAnimation("Dying_01", true);
                isDead = true;
            }
            else
            {
                animatorHandler.PlayerTargetAnimation(damageAnim, true);
            }





        }
        public void HealPlayer(float healAmount)
        {
            currentHealth = currentHealth + healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetCurrentHealth(currentHealth);
        }
        #endregion

        #region Stamina Related
        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        public void FillStaminaBar()
        {
            if (playerManger.isIntarecting)
            {
                staminaRegenTimer = 0;
            }

            if (!playerManger.isIntarecting&&!playerManger.isSprinting)

            {
                staminaRegenTimer += Time.deltaTime;
                if (playerManger.isIntarecting == false && staminaRegenTimer < 3f && currentStamina < maxStamina)
                {
                    currentStamina += (staminaregenrationPower / 10) * Time.deltaTime;
                    staminaBar.SetCurrentStamina(currentStamina);
                }
                else if (playerManger.isIntarecting == false && staminaRegenTimer > 3f && currentStamina < maxStamina)
                {
                    currentStamina += staminaregenrationPower * Time.deltaTime;
                    staminaBar.SetCurrentStamina(currentStamina);
                }
                else if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }
            }





        }
        public void TakeStaminaDamage(float damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
            if(currentStamina<=0)
            {
                currentStamina = 0;
            }
        }
        #endregion

        #region Mana RElated
        public float SetManaFromMaxManaLevel()
        {
            maxMana = manaLevel * 10;
            return maxMana;
        }

        public void DrainMana(float ManaCost)
        {
            currentMana= currentMana - ManaCost;
            if(currentMana<0)

            {
                currentMana= 0;
            }
            if(currentMana>maxMana)
            {
                currentMana= maxMana;   
            }
            manaBar.SetCurrentMana(currentMana);

        }

        public void FillManaBar()
        {
            if (playerManger.isIntarecting)
            {
                manaRegenTimer = 0;
            }

            if (!playerManger.isIntarecting)

            {
                manaRegenTimer += Time.deltaTime;
                if (playerManger.isIntarecting == false && manaRegenTimer    < 3f && currentMana < maxMana)
                {
                    currentMana += (manaRegenrationPower / 50) * Time.deltaTime;
                    manaBar.SetCurrentMana(currentMana);
                }
                else if (playerManger.isIntarecting == false && manaRegenTimer > 3f && currentMana < maxMana)
                {
                    currentMana +=( manaRegenrationPower/25) * Time.deltaTime;
                    manaBar.SetCurrentMana(currentMana);
                }
                else if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
            }





        }

        public void ManaRecovery(float manaAmout)
        {
            currentMana = currentMana + manaAmout;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            manaBar.SetCurrentMana(currentMana);
        }

        #endregion



        public void AddEXP(int expCountAdded)
        {
            expCount=expCount+expCountAdded;
        }




    }
}