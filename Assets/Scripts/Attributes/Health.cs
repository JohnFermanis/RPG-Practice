using RPG.Core;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace RPG.Attributes
{
    
    public class Health : MonoBehaviour
    {
        private bool isDead = false;
        [SerializeField] float health = 100.0f;
        private float Maxhealth;
        private BaseStats Stats = null;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
            Maxhealth = health;

            Stats = GetComponent<BaseStats>();
        }

        public bool IsDead() {  return isDead; }

        public void TakeDamage(float damage, GameObject instigator)
        {
            if (isDead) return;
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                AwardExperience(instigator);
                Die();
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();

            if(experience != null)
            experience.GainExperience(Stats.GetStat(Stat.Experience));
        }

        public float GetPercentage()
        {
            return (health / Maxhealth) * 100.0f;
        }
        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelAction();
        }
    }
}
