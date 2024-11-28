using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyStats enemyStats;

        public NavMeshAgent navMeshAgent;
        public Rigidbody rb;



        public State currentState;
        public State deadState;
        public CharacterStats currentTarget;



        public bool canBackStab;



        public int expAwardedOnDeath;



        [Header("A.I Settings")]
        public bool isPerformingAction;
        public bool isInteracting;
        public float detectionRadius=20;
        public float maximumDetectionAngle=50;
        public float minimumDetectionAngle=-50;
        public float maximumAttackRange=1.5f;
        public float currentRecoveryTime = 0;
        public float rotationSpeed = 15;


        [Header("Combat Flags")]
        public bool canDoCombo;

        


        private void Awake()
        {
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            navMeshAgent.enabled = false;
            backStabCollider=GetComponentInChildren<CriticalDamageCollider>();
        }
        private void Start()
        {
            
            rb.isKinematic = false;


        }
        private void Update()
        {
            HandleRecoveryTime();
            handleStateMachine();

            isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
            enemyAnimatorManager.anim.SetBool("isDead",enemyStats.isDead);
            canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");
        }
        private void FixedUpdate()
        {
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if ((currentTarget==null))
            {
                canBackStab = true;
            }
            else
            {
                canBackStab= false;
            }

        }
       





        private void handleStateMachine()
        {
            if(enemyStats.isDead)
            { 
                return;
            }

            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

                if (nextState!=null)
                {
                    switchTonextState(nextState);
                }


            }
        }
        private void switchTonextState(State state)
        {
            currentState = state;
        }
        private void HandleRecoveryTime()
        {
            if(currentRecoveryTime >0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if(isPerformingAction)
            {
                if(currentRecoveryTime<=0)
                {
                    isPerformingAction = false;
                    enemyAnimatorManager.anim.SetBool("isPerformingAction", false); 
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; //replace red with whatever color you prefer
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }


    }

}