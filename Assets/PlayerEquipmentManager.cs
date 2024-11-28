using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        public BlockingCollider blockingCollider;
        PlayerInventory playerInventory;

        private void Awake()
        {
            playerInventory = GetComponentInParent<PlayerInventory>();
        }

        public void OpenBlockingCollider()
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.handShield);
            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }

    }
}
