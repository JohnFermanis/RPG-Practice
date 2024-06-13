using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed=5.0f;
        [SerializeField] float targetHeight = 1.5f;
        [SerializeField] bool isHoming=true;
        [SerializeField] float LifeDuration = 8.0f;
        [SerializeField] GameObject ImpactVFX = null;
        Health currentTarget = null;
        GameObject instigator = null;
        float damage = 0.0f;

        private void Start()
        {
            LookAtTarget();
            Destroy(this.gameObject, LifeDuration);
        }
        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (isHoming && currentTarget != null)
                LookAtTarget();
           
        }

        public void SetTarget(Health target, float attackStat, GameObject instigator)
        {
            currentTarget = target;
            damage = attackStat;
            this.instigator = instigator;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (currentTarget.IsDead() || currentTarget == null)
            {
                Debug.Log("called");
                Destroy(this.gameObject);
            }

            if(other.gameObject == currentTarget.gameObject)
            {
                currentTarget.TakeDamage(damage,instigator);
                Instantiate(ImpactVFX,transform.position,transform.rotation);
                Destroy(this.gameObject);
            }
        }

        private void LookAtTarget()
        {
            transform.LookAt(currentTarget.transform.position + Vector3.up * targetHeight);
        }
    }
}