using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class Interactables : MonoBehaviour
    {
        public float radius=0.6f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }



        public virtual void Interact(PLayerManager playerManager)
        {
            //player intereact 

            Debug.Log("you touch a obj");
        }
    }
}
