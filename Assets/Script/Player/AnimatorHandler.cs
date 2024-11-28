using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class AnimatorHandler : AnimatorManager
    {

        PLayerManager playerManager;
        PlayerStats playerStats;
        InputHandller inputHandler;
        PlayerLocomotion playerLocomotion;

        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            playerManager=GetComponentInParent<PLayerManager>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();    
            inputHandler = GetComponentInParent<InputHandller>();   
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
            playerStats = GetComponentInParent<PlayerStats>();

        }

        public void UpadateAnimatorValues(float verticalMov,float horizontalMov,bool isSprining)
        {
            #region Vertical
            float v = 0;

            if(verticalMov>0&&verticalMov<0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMov>0.55f)
            {
                v = 1;
            }
            else if (verticalMov<0 && verticalMov>-0.55f)
            {
                v = -.5f;

            }
            else if (verticalMov<-.55f)
            {
                v= -1;
            }
            else
            { v = 0; }

            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMov > 0 && horizontalMov < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMov > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMov < 0 && horizontalMov > -0.55f)
            {
                h = -.5f;

            }
            else if (horizontalMov < -.55f)
            {
                h = -1;
            }
            else
            { h = 0; }

            #endregion

           
            if (isSprining&&inputHandler.MoveAmount>0)
            {
                
                v = 2;
                h = horizontalMov;
            }

            anim.SetFloat(vertical,v,.1f,Time.deltaTime);   
            anim.SetFloat(horizontal,h,.1f,Time.deltaTime);


            


        }

        

        public void CanRotate()
        {
            anim.SetBool("canRotate",true);
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
        public void EnableJumpflag()
        {
            playerManager.jumpFlag = true;
        }
        public void DisableJumpflag()
        {
            playerManager.jumpFlag = false;
        }

        public void EnableIsParring()
        {
            playerManager.isParring = true;
        }
        public void DisnableIsParring()
        {
            playerManager.isParring = false;
        }

        public void EnableCanBeRiposted()
        {
            playerManager.canBeRiposted = true;
        }

        public void DisableCanBeRiposted()
        {
            playerManager.canBeRiposted = true;
        }
        public override void TakeCriticalDamageAnimation()
        {
            playerStats.TakeDamageNoAnim(playerManager.pendingCriticalDamage);
            playerManager.pendingCriticalDamage = 0;
        }

        private void OnAnimatorMove()
        {
            if (playerManager.isIntarecting == false)
                return;


            float delta=Time.deltaTime;
            playerLocomotion.rb.drag = 0;
            Vector3 deltaPos=anim.deltaPosition;
            if (playerManager.jumpFlag)
            {

            }
            else
            {
                deltaPos.y = 0;
            }
            Vector3 velocity = deltaPos / delta;
            playerLocomotion.rb.velocity = velocity;    

        }


    }
}
