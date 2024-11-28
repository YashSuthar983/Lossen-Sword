using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


namespace SG
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformposition;
        public LayerMask ignoreLayer;
        public LayerMask environmentLayer;
        private Vector3 cameraFollowVelociy=Vector3.zero;


        public static CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.3f;

        private float targetPos;
        private float deafaultPos;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivt = -35;
        public float maximumPivot = 35;


        public float cameraSphereRadius = 0.2f;
        public float cameraCollisonOffse = 0.2f;
        public float minimumCollisionOffset = 0.2f;

        public float lockPivotPosition = 2.25f;
        public float unlockPivotPosition = 1.65f;

        public CharacterManager currentLockOnTarget;




        List<CharacterManager> availableTargets=new List<CharacterManager>();   
        public float maximumLockOnTarget=30;


        public CharacterManager nearestLockOntarget;
        public CharacterManager leftLockTarget;
        public CharacterManager rightLockTarget;
        

        InputHandller inputHandller;
        PLayerManager playerManger;
        




        private void Awake()
        {
            playerManger=FindAnyObjectByType<PLayerManager>();  
            inputHandller=FindAnyObjectByType< InputHandller>();
            singleton = this;
            myTransform=transform;
            deafaultPos = cameraTransform.localPosition.z;
            ignoreLayer = ~(1 << 8 | 1 << 9 | 1 << 10|1<<12|1<<13);
            targetTransform=FindObjectOfType<PLayerManager>().transform;    

        }
        private void Start()
        {
            environmentLayer = LayerMask.NameToLayer("environment");
        }





        public void FollowTarget(float delta)
        {
            
            Vector3 targetPos= Vector3.SmoothDamp(myTransform.position,targetTransform.position,ref cameraFollowVelociy,delta/followSpeed);
            
            
            myTransform.position = targetPos;

            HandleCamCollision(delta);
        }
        public void HandleCameraRotation(float delta,float mouseXin,float mouseYin)
        {
            if(inputHandller.lockOnFlag==false)
            {
                lookAngle += (mouseXin * lookSpeed) / delta;
                pivotAngle -= (mouseYin * pivotSpeed) / delta;
                pivotAngle = Mathf.Clamp(pivotAngle, minimumPivt, maximumPivot);

                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotation = Quaternion.Euler(rotation);
                myTransform.rotation = targetRotation;

                rotation = Vector3.zero;
                rotation.x = pivotAngle;

                targetRotation = Quaternion.Euler(rotation);
                cameraPivotTransform.localRotation = targetRotation;

            }
            else
            {
                //float velocity = 0;
                Vector3 dir = currentLockOnTarget.transform.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = currentLockOnTarget.transform.position - cameraPivotTransform.position;
                dir.Normalize();
                targetRotation = Quaternion.LookRotation(dir);
                Vector3 eularAngle=targetRotation.eulerAngles;
                eularAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eularAngle;
            }
            
        }
        private void HandleCamCollision(float delta)
        {
            targetPos = deafaultPos;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();
            if(Physics.SphereCast(cameraPivotTransform.position,cameraSphereRadius,direction,out hit,Mathf.Abs(targetPos),ignoreLayer))
            {
                float dis =Vector3.Distance(cameraPivotTransform.position,hit.point);
                targetPos = -(dis - cameraCollisonOffse);

            }
            if(Mathf.Abs(targetPos)<minimumCollisionOffset)
            {
                targetPos = -minimumCollisionOffset;
            }

            cameraTransformposition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, delta / 0.2f);
            cameraTransform.localPosition=cameraTransformposition;

        }
        public  void HandleLockOn()
        {
            availableTargets.Clear();

            float shortDis=Mathf.Infinity;
            float shortestDistanceFromLeftTarget=-Mathf.Infinity;
            float shortestDistanceFromRightTarget=Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 28);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();
                
                if(character != null)
                {
                    RaycastHit hit;
                    Vector3 lockTargetDir=character.transform.position-targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position ,character.transform.position);
                    
                    float viewAngle=Vector3.Angle(lockTargetDir,cameraTransform.forward);
                    if(character.transform.root!=targetTransform.transform.root&&viewAngle>-50&&viewAngle<50&&distanceFromTarget<=maximumLockOnTarget) 
                    
                    {
                        if(Physics.Linecast(playerManger.LockOnTRansform.position,character.LockOnTRansform.position,out hit))
                        {
                            Debug.DrawLine(playerManger.LockOnTRansform.position,character.LockOnTRansform.position);

                            if (hit.transform.gameObject.layer == environmentLayer)
                            {

                            }
                            else
                            {
                                availableTargets.Add(character);

                            }
                        }
                    }
                }
            
            }

            for (int k = 0; k < availableTargets.Count; k++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);
            
                if(distanceFromTarget<shortDis)
                {
                    shortDis = distanceFromTarget;
                    nearestLockOntarget = availableTargets[k];
                }
                if (inputHandller.lockOnFlag)
                {
                    //Vector3 relativeEnemyPos = currentLockOnTarget.transform.InverseTransformPoint(availableTargets[k].transform.position);
                    //var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;
                    //var distanceFromRightTarget= currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;
                    Vector3 relativeEnemyPos = inputHandller.transform.InverseTransformPoint(availableTargets[k].transform.position);
                    var distanceFromLeftTarget = relativeEnemyPos.x;
                    var distanceFromRightTarget = relativeEnemyPos.x;

                    if (relativeEnemyPos.x <=0 && distanceFromLeftTarget>shortestDistanceFromLeftTarget&& availableTargets[k]!=currentLockOnTarget)
                    {
                        shortestDistanceFromLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = availableTargets[k];
                    }

                    else if (relativeEnemyPos.x >= 0 && distanceFromRightTarget < shortestDistanceFromRightTarget && availableTargets[k] != currentLockOnTarget)
                    {
                        shortestDistanceFromRightTarget = distanceFromRightTarget;
                        rightLockTarget = availableTargets[k];
                    }


                }
                    
            }


        }
        public void ClearLockOntarget()
        {
            availableTargets.Clear();
            nearestLockOntarget = null;
            currentLockOnTarget = null;
        }
        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockPivotPosition);
            Vector3 newUnlockedPosition= new Vector3(0, unlockPivotPosition);

            if(currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition=Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition,newLockedPosition,ref velocity,Time.deltaTime);

            }
            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);

            }
        }
    }
    
}
