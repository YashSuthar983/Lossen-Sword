using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class CharacterManager : MonoBehaviour
    {
        public Transform LockOnTRansform;

        public CriticalDamageCollider backStabCollider;
        public CriticalDamageCollider RiposteCollider;


        public bool canBeRiposted;
        public bool canBeParried;
        public bool isParring;
        public bool isBlocking;

        public int pendingCriticalDamage;

        
    }
}
