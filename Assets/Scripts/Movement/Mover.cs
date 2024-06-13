using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Attributes;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        [SerializeField]
        private NavMeshAgent moverAgent;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        Health health;

        [SerializeField]
        float maxSpeed = 6.0f;

        private void Update()
        {
            if(health.IsDead())
                moverAgent.enabled = false;
            //Debug.Log("Remaining Distance is" + moverAgent.remainingDistance);
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }


        public void UpdateAnimator()
        {
            Vector3 CurrentVelocity = transform.InverseTransformDirection(moverAgent.velocity);

            animator.SetFloat("ForwardVelocity", CurrentVelocity.z);
            //Debug.Log(CurrentVelocity);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            moverAgent.isStopped = false;
            moverAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            moverAgent.destination = destination;
            
        }

        public void Cancel() {
  
                moverAgent.isStopped = true;
        }
    }
}
