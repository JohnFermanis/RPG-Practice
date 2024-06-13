using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;

        
        float deltaAlpha;

        /*public IEnumerator FadeInOut()
        {
            yield return FadeIn(fadeTime);
            yield return new WaitForSeconds(0.2f);
            yield return FadeOut(fadeTime);
        }*/

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha!=1.0f)
            {
                deltaAlpha = Time.deltaTime/time;
                canvasGroup.alpha += deltaAlpha;
                yield return null;
            }
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha != 0.0f)
            {
                deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha -= deltaAlpha;
                yield return null;
            }
        }
    }
}
