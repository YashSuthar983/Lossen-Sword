using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PLayerManager : CharacterManager
{

    InputHandller inputHandller;
    Animator anim;
    CameraHandler cameraHandler;
    PlayerLocomotion playerLocomotion;
    InteractableUI interactableUI;
    PlayerStats playerStats;
    AnimatorHandler playerAnimator;




    public GameObject interactableUITextGameOBJ;
    public GameObject itemInteractableNameUIGameOBJ;
    public GameObject itemInteractableIcon;




    [Header("Player Flags")]
    public bool isIntarecting;
    public bool isSprinting;
    public bool jumpFlag;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;






    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        interactableUI = FindObjectOfType<InteractableUI>();
        anim = GetComponentInChildren<Animator>();
        inputHandller = GetComponent<InputHandller>();
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        playerAnimator = GetComponentInChildren<AnimatorHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 120;

        float delta = Time.deltaTime;
        isIntarecting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        anim.SetBool("IsInAir", isInAir);
        anim.SetBool("isDead", playerStats.isDead);
        anim.SetBool("isBlocking", isBlocking);
        playerAnimator.canRotate = anim.GetBool("canRotate");

        inputHandller.TickINPUT(delta);
        playerLocomotion.HandleJump();
        playerLocomotion.HandleRollandSprint(delta);
        playerStats.FillStaminaBar();
        playerStats.FillManaBar();
        CheckForInterractable();


    }


    private void FixedUpdate()
    {

        float delta = Time.fixedDeltaTime;
        playerLocomotion.HandleMovemet(delta);
        playerLocomotion.HandleFalling(delta, playerLocomotion.MoveDirection);
        playerLocomotion.HandleRotation(delta);
    }

    private void LateUpdate()
    {
        //player falgs
        inputHandller.rollFlag = false;

        //player inputs 
        inputHandller.y_Input = false;
        inputHandller.x_Input = false;
        inputHandller.a_Input = false;
        inputHandller.b_Input = false;
        inputHandller.f_Input = false;
        inputHandller.parry_Input = false;

        //player movements
        inputHandller.d_Pad_Down = false;
        inputHandller.d_Pad_Left = false;
        inputHandller.d_Pad_Right = false;
        inputHandller.d_Pad_Up = false;
        inputHandller.jump_Input = false;

        float delta = Time.fixedDeltaTime;

        if ((cameraHandler != null))
        {

            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandller.MouseX, inputHandller.MouseY);

        }

        if (isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }
    }

    public void CheckForInterractable()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.2f, transform.forward, out hit, 1, cameraHandler.ignoreLayer))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactables interactablesObj = hit.collider.GetComponent<Interactables>();

                if (interactablesObj != null)
                {
                    string interactableText = interactablesObj.interactableText;
                    //set ui text to obj text
                    interactableUI.interactableText.text = interactableText;
                    interactableUITextGameOBJ.SetActive(true);

                    if (inputHandller.f_Input)
                    {
                        hit.collider.GetComponent<Interactables>().Interact(this);
                    }
                }

            }
        }
        else
        {
            if (interactableUITextGameOBJ != null)
            {
                interactableUITextGameOBJ.SetActive(false);

            }
            if (itemInteractableNameUIGameOBJ != null && inputHandller.f_Input)
            {
                itemInteractableNameUIGameOBJ.SetActive(false);
                itemInteractableIcon.SetActive(false);

            }
        }


    }

    public void OpenChestinteraction(Transform playerStandPoint)
    {
        playerLocomotion.rb.velocity = Vector3.zero;
        transform.position= playerStandPoint.transform.position;
        playerAnimator.PlayerTargetAnimation("Open Chest", true);



    }


}
