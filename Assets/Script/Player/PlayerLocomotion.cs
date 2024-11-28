using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


namespace SG
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerStats playerStats;
        PLayerManager playerManager;
        Transform CameraObject;
        InputHandller InputHandler;
        CameraHandler CameraHandler;
        
        public Vector3 MoveDirection;


        [HideInInspector]
        public Transform myTransform;
        [SerializeField]
        public AnimatorHandler animHandler;




        public  Rigidbody rb;
        public GameObject NormalCamera;
        [Header("Gorund &air Detetction ststs")]
        [SerializeField]
        float groundDetetctionRayCastPoint = 0.5f;
        [SerializeField]
        float minimumDistanceNeedToBegginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;


        [Header("Stats")]
        [SerializeField]
        float walkInSpeed = 2;
        [SerializeField]
        float movemetSpeed = 4;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        float sprintSpeed = 6;
        [SerializeField]
        float fallingSpeed = 98;
        [SerializeField]
        public float jumpForce=.5f;

        [Header("Loco Stamina Costs")]
        [SerializeField]
        float sprintStaminaCost;
        [SerializeField]
        float rollStaminaCost;
        [SerializeField]
        float dodgeStaminaCost;
        [SerializeField]
        float jumpStaminaCost;


        [Header("Loco Override Stamina Damage Costs")]
        [SerializeField]
        float roll_sprint_dodge_OverrideDamageCost;
        [SerializeField]
        float jumpOverrideDamageCost;




        void Start()
        { 
            playerStats = GetComponent<PlayerStats>();
            CameraHandler = FindAnyObjectByType<CameraHandler>();
            playerManager=GetComponent<PLayerManager>();
            rb = GetComponent<Rigidbody>();
            InputHandler=GetComponent<InputHandller>();
            CameraObject=Camera.main.transform;
            myTransform=transform;
            animHandler=GetComponentInChildren<AnimatorHandler>();
            animHandler.Initialize();


            playerManager.isGrounded=true;
            ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        }


        

        #region Movment
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleRotation(float delta)
        {
            if (animHandler.canRotate)
            {
                if (InputHandler.lockOnFlag)
                {
                    if (InputHandler.sprintFlag || InputHandler.rollFlag)
                    {
                        Vector3 targetdir = Vector3.zero;
                        targetdir = CameraHandler.cameraTransform.forward * InputHandler.Vertical;
                        targetdir += CameraHandler.cameraTransform.right * InputHandler.Horizontal;
                        targetdir.Normalize();
                        targetdir.y = 0;

                        if (targetdir == Vector3.zero)
                        {
                            targetdir = transform.forward;
                        }

                        Quaternion tr = Quaternion.LookRotation(targetdir);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                        transform.rotation = targetRotation;
                    }
                    else
                    {
                        Vector3 rotationDir = MoveDirection;
                        rotationDir = CameraHandler.currentLockOnTarget.transform.position - transform.position;
                        rotationDir.Normalize();
                        rotationDir.y = 0;
                        Quaternion tr = Quaternion.LookRotation(rotationDir);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                        transform.rotation = targetRotation;
                    }


                }

                else
                {
                    Vector3 targetDir = Vector3.zero;
                    float moveOverride = InputHandler.MoveAmount;

                    targetDir = CameraObject.forward * InputHandler.Vertical;
                    targetDir += CameraObject.right * InputHandler.Horizontal;

                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero)
                    {
                        targetDir = myTransform.forward;

                    }


                    float rs = rotationSpeed;

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotaion = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);


                    myTransform.rotation = targetRotaion;

                }
            }

            

        }

        public void HandleMovemet(float delta)
        {
            if (InputHandler.rollFlag)
                return;

            if (playerManager.isIntarecting)
                return;
            

            MoveDirection = CameraObject.forward * InputHandler.Vertical;
            MoveDirection += CameraObject.right * InputHandler.Horizontal;

            MoveDirection.Normalize();
            MoveDirection.y = 0;


            float speed = movemetSpeed;
            
            if(InputHandler.sprintFlag&& InputHandler.MoveAmount>0.5f)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                MoveDirection*=speed;
                playerStats.TakeStaminaDamage(sprintStaminaCost);
                if (playerStats.currentStamina <= 0)
                {
                    playerStats.TakeDamageNoAnim(roll_sprint_dodge_OverrideDamageCost);
                }
            }
            else
            {
                if(InputHandler.MoveAmount<0.5)
                {
                    MoveDirection *= walkInSpeed;
                    playerManager.isSprinting=false;
                }
                else
                {
                    MoveDirection *= speed;
                    playerManager.isSprinting = false;

                }

            }

            
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(MoveDirection, normalVector);
            rb.velocity = projectedVelocity;

            if (InputHandler.lockOnFlag && InputHandler.sprintFlag==false)
            {
                animHandler.UpadateAnimatorValues(InputHandler.Vertical ,InputHandler.Horizontal, playerManager.isSprinting);

            }
            else
            {
                animHandler.UpadateAnimatorValues(InputHandler.MoveAmount, 0, playerManager.isSprinting);

            }


            
        }

        public void HandleRollandSprint(float delta)
        {
            if(playerManager.isIntarecting)
            {
                return;
            }

            if (InputHandler.rollFlag)
            {
                MoveDirection = CameraObject.forward * InputHandler.Vertical;
                MoveDirection += CameraObject.right * InputHandler.Horizontal;

                
                
                

                if (InputHandler.MoveAmount > 0)
                {
                    animHandler.PlayerTargetAnimation("Rolling", true );
                    MoveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(MoveDirection);
                    myTransform.rotation = rollRotation;
                    playerStats.TakeStaminaDamage(rollStaminaCost);

                }
                else 
                {
                    playerStats.TakeStaminaDamage(dodgeStaminaCost);

                    animHandler.PlayerTargetAnimation("Backstep", true); 
                }


                if (playerStats.currentStamina <= 0)
                {
                    playerStats.TakeDamageNoAnim(roll_sprint_dodge_OverrideDamageCost);
                }
            }


        }


        public void HandleFalling(float delta,Vector3 MoveDirection)
        {
           if(playerManager.jumpFlag)
                return;

            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin=myTransform.position;
            origin.y += groundDetetctionRayCastPoint;

            if(Physics.Raycast(origin,myTransform.forward,out hit,0.4f))
            {
                MoveDirection = Vector3.zero;
            }

            if(playerManager.isInAir)
            {
                rb.AddForce(-Vector3.up * fallingSpeed);
                rb.AddForce(MoveDirection * fallingSpeed / 6);
            }

            Vector3 dir=MoveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition=myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeedToBegginFall, Color.red,0.1f,false);

            
            if(Physics.Raycast(origin,-Vector3.up,out hit,minimumDistanceNeedToBegginFall,ignoreForGroundCheck))
            {
                normalVector=hit.normal;
                Vector3 tp=hit.point;
                playerManager.isGrounded = true; 
                
                targetPosition.y=tp.y;

                if(playerManager.isInAir)
                {

                    if(inAirTimer>0.5f)
                    {
                        Debug.Log("you were in air for"+inAirTimer);
                        animHandler.PlayerTargetAnimation("Land",true);

                        inAirTimer = 0;
                    }
                    else
                    {
                        animHandler.PlayerTargetAnimation("Empty", false); 
                        inAirTimer = 0;

                    }


                    playerManager.isInAir=false;

                }

            }
            else
            {
                if(playerManager.isGrounded) 
                {
                    playerManager.isGrounded=false;
                }

                if(playerManager.isInAir==false)
                {
                    if(playerManager.isIntarecting==false)
                    {
                        animHandler.PlayerTargetAnimation("Falling", true );

                    }

                    


                    Vector3 vel =rb.velocity;
                    vel.Normalize();
                    rb.velocity = vel * (movemetSpeed / 2);
                    playerManager.isInAir=true;

                }
                else if(playerManager.isInAir == true&&inAirTimer>.5f)
                {
                    animHandler.PlayerTargetAnimation("Falling", true );
                    
                }


            }

            if (playerManager.isGrounded)
            {
                if((playerManager.isIntarecting||InputHandler.MoveAmount>0))
                {
                    myTransform.position=Vector3.Lerp(myTransform.position,targetPosition,Time.deltaTime);
                }
                else
                {
                    myTransform.position=targetPosition;
                }
            }

        }


        public void HandleJump()
        {
            
            if (playerManager.isIntarecting)
                return;
            if(InputHandler.lockOnFlag) 
                return;
            if(InputHandler.jump_Input)
            {
                if(InputHandler.MoveAmount > 0)
                {
                    playerStats.TakeStaminaDamage(jumpStaminaCost);

                    MoveDirection = CameraObject.forward * InputHandler.Vertical;
                    MoveDirection += CameraObject.right * InputHandler.Horizontal;
                  
                    animHandler.PlayerTargetAnimation("Running Jump", true );
                    MoveDirection.y = 0;
                    
                    quaternion jumpRotation = Quaternion.LookRotation(MoveDirection);
                    myTransform.rotation = jumpRotation;

                    if (playerStats.currentStamina <= 0)
                    {
                        playerStats.TakeDamageNoAnim(jumpOverrideDamageCost);
                    }

                }
            }
        }


        #endregion


    }
}


