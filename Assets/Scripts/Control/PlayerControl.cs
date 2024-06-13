using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;

namespace RPG.Control
{
    public class PlayerControl : MonoBehaviour
    {

        [SerializeField]
        private Movement.Mover movement;

        [SerializeField]
        private Combat.Fighter fighter;

        [SerializeField]
        Health health;


        void Start()
        {

        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            //Debug.Log("Nothing to do");

        }


        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(FireRayToMouse());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)){
                    fighter.Attack(target.gameObject);
                }
                return true;

            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(FireRayToMouse(), out hit))
            {
                if (hit.point==null)
                    return false;
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                movement.StartMoveAction(hit.point,1.0f);
                return true;
            }
            return false;
        }

        private Ray FireRayToMouse()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    }
}


