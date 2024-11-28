using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{

    public class EnemyHealthBar : MonoBehaviour
    {
        public Slider slider;
        public Camera cam;
        public Transform target;
        public Vector3 offset;
        public GameObject canva;

        

        public void SetMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(float currentHealth)
        {
            slider.value = currentHealth;
        }

        public void LateUpdate()
        {
            canva.transform.rotation=cam.transform.rotation;
            canva.transform.position = target.position + offset;
        }

    }
}
