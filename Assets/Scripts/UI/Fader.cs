using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Fader : MonoBehaviour
    {
        private Image _bg;
        
        void Awake()
        {
            _bg = transform.GetComponent<Image>();
            
            /*// Fader is invisible before use
            var empty = Color.black;
            empty.a = 0;
            _bg.color = empty;*/
        }
        
        public IEnumerator FadeOut(float time)
        {
            if (time <= 0) yield return null;
            
            var elapsed = 0f;
            var targetColor = Color.black;
            while (elapsed < time)
            {
                var deltaTime = Time.deltaTime;
                elapsed += deltaTime;
                targetColor.a = elapsed / time;
                _bg.color = targetColor;
                yield return new WaitForSeconds(deltaTime);
            }
        }

        public IEnumerator FadeOut(float time, float delay)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FadeOut(time));
        }
        
        public IEnumerator FadeIn(float time)
        {
            if (time <= 0) yield return null;
            
            var elapsed = 0f;
            var targetColor = Color.black;
            while (elapsed < time)
            {
                var deltaTime = Time.deltaTime;
                elapsed += deltaTime;
                targetColor.a = 1 - elapsed / time;
                _bg.color = targetColor;
                yield return new WaitForSeconds(deltaTime);
            }
        }

        public IEnumerator FadeIn(float time, float delay)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FadeIn(time));
        }
    }
}
