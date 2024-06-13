using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] float fadeTime = 0.3f;
        [SerializeField] float fadeWaitTime = 0.0f;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destinationIdentifier;
        private void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition() {

            if(sceneToLoad == -1)
            {
                Debug.LogError("Load scene not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);


            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeIn(fadeTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeOut(fadeTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            // In case Navmesh agent conflicts with position, use:
            // player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal==this) continue;
                if (portal.destinationIdentifier == this.destinationIdentifier)
                return portal;
            }

            return null;
        }
    }
}
