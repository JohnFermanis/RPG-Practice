using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmosRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i=0; i < transform.childCount; i++)
            {

                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmosRadius);
                Gizmos.DrawLine(GetWaypoint(i),GetWaypoint(GetNextIndex(i)) );
            }
        }

        public int GetNextIndex(int i)
        {
            if(transform.childCount==i+1)
                return 0;
            return i+1;
        }
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}
