using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5.0f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag=="Player") {

                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickUp(false); //Hide
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true); //Show
        }

        private void ShowPickUp(bool show)
        {
            this.GetComponent<CapsuleCollider>().enabled = show;
            foreach(GameObject child in transform)
            {
                child.gameObject.SetActive(show);
            }
         
        }

        
    }
}