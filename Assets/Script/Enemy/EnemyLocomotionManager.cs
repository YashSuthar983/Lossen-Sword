using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SG
{

    public class EnemyLocomotionManager : MonoBehaviour
    {

        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;



        Vector3 normalVector;



        private void Awake()
        {
            enemyManager=GetComponent<EnemyManager>();
            enemyAnimatorManager=GetComponentInChildren<EnemyAnimatorManager>();
        }
        


        /*public void HandleDetection ()
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < collider.Length; i++)
            {
                CharacterStats characterStats = collider[i].transform.GetComponentInParent<CharacterStats>();

                if(characterStats != null)
                {

                    Vector3 targetDir=characterStats.transform.position-transform.position;
                    float viewableAngle=Vector3.Angle(targetDir,transform.forward);


                    if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle<enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterStats;
                    }
                }
                
            }


        }*/

        

       

    }
}