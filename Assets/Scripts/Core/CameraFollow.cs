using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform FollowTarget;

        // Update is called once per frame
        void LateUpdate()
        {
            this.transform.position = FollowTarget.position;
        }
    }

}
