using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float attackStat = 2.0f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";


        public void SpawnWeapon(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Transform currentHand = GetHandTransform(rightHand, leftHand);

            var OverrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (weaponPrefab != null)
            {
                GameObject weapon = Instantiate(weaponPrefab, currentHand);
                weapon.name = weaponName;
            }
            if (animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;
            else if (OverrideController != null) //if not null, change back to default weapon
                animator.runtimeAnimatorController = OverrideController.runtimeAnimatorController;
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);

            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);   
            }
            if (oldWeapon == null) return;
            

            oldWeapon.name= "DESTROYING";
            Destroy(oldWeapon.gameObject);

        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            if (isRightHanded)
                return rightHand;
            else
                return leftHand;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform leftHand, Transform rightHand, Health target, GameObject instigator)
        {
            Projectile projectileInstance = Instantiate(projectile,GetHandTransform(rightHand,leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, attackStat, instigator);
        }
        public float GetRange()
        {
            return weaponRange;
        }

        public float GetAttackStat()
        {
            return attackStat;
        }
    }
}