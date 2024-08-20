using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Attributes;
using RPG.Stats;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, IStatModifier
    {
        Health target;

        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon=null;
        Weapon currentWeapon = null;
        [SerializeField] float timeBetweenAttacks=1.0f;
        float timeSinceLastAttack = 100.0f;


        [SerializeField]
        Movement.Mover Mover;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!IsInRange())
            {
                Mover.MoveTo(target.transform.position,1.0f);
            }
            else
            {
                Mover.Cancel();
                transform.LookAt(target.transform);
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            //Defines the Attack speed (ASPD)
            if (timeBetweenAttacks <= timeSinceLastAttack)
            {
                // This will trigger the animation Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0.0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool IsInRange()
        {
            
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            target=null;
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Mover>().Cancel();
            
        }

        //Animation event Hit
        void Hit()
        {
            if (target != null)
            target.TakeDamage(CalculateDamage(), gameObject);
        }

        void Shoot()
        {
            if (target != null)
                currentWeapon.LaunchProjectile(leftHandTransform, rightHandTransform, target, gameObject, CalculateDamage());
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.SpawnWeapon(rightHandTransform, leftHandTransform, animator);

        }

        private float CalculateDamage() {

            return GetComponent<BaseStats>().GetStat(Stat.Attack);
        }

        public Health ReturnTargetHealth()
        {
            return target;
        }

        public IEnumerable<float> GetStatAdders(Stat stat)
        {
            if(stat == Stat.Attack)
            {
                yield return currentWeapon.GetAttackStat();
            }
        }

        public IEnumerable<float> GetStatMultipliers(Stat stat)
        {
            if (stat == Stat.Attack)
            {
                yield return currentWeapon.GetPercentageBuff();
            }
        }
    }
}
