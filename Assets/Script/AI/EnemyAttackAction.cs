using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace SG
{
    [CreateAssetMenu(menuName ="AI/Enemy Action/Attack Action")]

    public class EnemyAttackAction : EnemyActions
    {
        public bool canCombo;

        public EnemyAttackAction combooAction;
        public int attackscore = 3;
        public float recoveryTime = 2;

        public float maximumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 3;

    }
}