using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaopnSlotManger : MonoBehaviour
{
    DamageCollider HandDamageCollider_Left;
    DamageCollider HandDamageCollider_Right;
    public GameObject enemyWeapon_Left;
    public GameObject enemyWeapon_Right;
    private void Start()
    {
        HandDamageCollider_Left = enemyWeapon_Left.GetComponentInChildren<DamageCollider>();
        HandDamageCollider_Right = enemyWeapon_Right.GetComponentInChildren<DamageCollider>();
        HandDamageCollider_Left.characterManager=GetComponentInParent<CharacterManager>();
        HandDamageCollider_Right.characterManager=GetComponentInParent<CharacterManager>(); 

    }
    public void OpenHandDamageCollider_Left()
    {
        HandDamageCollider_Left.EnableDamageCollider();
    }

    public void CloseHandDamageCollider_Left()
    {
        HandDamageCollider_Left.DisableDamageCollider();

    }
    public void OpenHandDamageCollider_Right()
    {
        HandDamageCollider_Right.EnableDamageCollider();
    }

    public void CloseHandDamageCollider_Right()
    {
        HandDamageCollider_Right.DisableDamageCollider();

    }
}
