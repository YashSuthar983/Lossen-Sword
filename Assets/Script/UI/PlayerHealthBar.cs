using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{

    public class PlayerHealthBar : MonoBehaviour
    {
        public Slider slider;

        

        public void SetMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(float currentHealth)
        {
            slider.value = currentHealth;
        }

    }
}
