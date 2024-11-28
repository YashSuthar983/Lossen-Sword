using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EXPCount : MonoBehaviour
    {
        public Text expCountText;

        
        public void UpdateExpCount(int expcount)
        {
            expCountText.text = expcount.ToString();
        }

    }
}
