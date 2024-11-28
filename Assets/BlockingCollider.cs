using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class BlockingCollider : MonoBehaviour
    {
        public BoxCollider blockingColliders;

        public float blockingPhysicalDamageAbsorption;

        private void Awake()
        {
            blockingColliders = GetComponent<BoxCollider>();
        }


        public void SetColliderDamageAbsorption(ShieldItem shield)
        {
            if (shield != null)
            {
                blockingPhysicalDamageAbsorption = shield.physicalDamageAbsorption;
            }
        }

        public void EnableBlockingCollider()
        {
            blockingColliders.enabled = true;
        }

        public void DisableBlockingCollider()
        {
            blockingColliders.enabled = false;
        }


    }


}