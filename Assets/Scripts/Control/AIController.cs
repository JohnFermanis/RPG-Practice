using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        Health health;

        [SerializeField]
        Mover mover;

        [SerializeField]
        float chaseDistance = 5.0f;

        [SerializeField]
        Fighter fighter;

        [SerializeField]
        PatrolPath patrolPath;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = 100.0f;

        float timeSinceArrivedAtWaypoint = 100.0f;

        [SerializeField]
        float waypointDwellTime=2.0f;

        [SerializeField]
        float timeSuspecting = 2.0f;

        [SerializeField]
        float waypointTolerance = 1.0f;

        [SerializeField]
        int currentWaypointIndex = 0;


        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.5f;

        GameObject player;
        private void Start()
        {
            guardPosition = transform.position;
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (player != null)
            {
                if (InChaseRange(player) && fighter.CanAttack(player))
                {
                    timeSinceLastSawPlayer = 0.0f;
                    AttackBehaviour();
                }
                else if (timeSinceLastSawPlayer <= timeSuspecting)
                {
                    SuspicionBehaviour();
                }
                else
                {
                    PatrolBehaviour();
                }

                UpdateTimers();
            }

        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {

            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
                
            }

            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
                mover.StartMoveAction(nextPosition,patrolSpeedFraction);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex=patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoit = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoit < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            fighter.Cancel();
            //GetComponent<ActionScheduler>().CancelAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InChaseRange(GameObject target)
        {
            if(Vector3.Distance(target.transform.position, this.transform.position)<=chaseDistance)
                return true;
            else return false;

        }

        private void OnDrawGizmosSelected()
        {
           Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
