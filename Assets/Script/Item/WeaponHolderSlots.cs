using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SG
{
    public class WeaponHolderSlots : MonoBehaviour
    {
        public Transform parentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

        public GameObject currentWeaponModel;
        public GameObject currentShieldModel;

        #region Weapon Loader and Destroyer
        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }
        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }
        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();

            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;

                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;




            }


            currentWeaponModel = model;

        }
        #endregion

        #region Shield Loader And Destroyer

        public void  UnloadShield()
        {
            if (currentShieldModel != null)
            {
                currentShieldModel.SetActive(false);
            }
        }
        public void UnloadShieldAndDestroy()
        {
            if (currentShieldModel != null)
            { Destroy(currentShieldModel); }
        }
        public void LoadShieldModel(ShieldItem shieldItem)
        {
            UnloadShieldAndDestroy();

            if (shieldItem == null)
            {
                UnloadShield();
                return;
            }

            GameObject model = Instantiate(shieldItem.modelPrefab) as GameObject;
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;

                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }
        }
        #endregion
    }
}
