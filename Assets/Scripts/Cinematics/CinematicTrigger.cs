using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool TriggeredOnce=false;

        private void OnTriggerEnter(Collider other)
        {
            if (!TriggeredOnce && other.gameObject.tag == "Player")
            {
                BeginCutscene();
            }
        }

        private void BeginCutscene()
        {
            GetComponent<PlayableDirector>().Play();
            TriggeredOnce = true;
        }
    }
}