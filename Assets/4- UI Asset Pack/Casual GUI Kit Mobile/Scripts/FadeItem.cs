using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace hardartcore.CasualGUI
{
    public class FadeItem : MonoBehaviour
    {
        public float StartAnimationAfter = 0.3f;
        public float AnimationDuration = 0.3f;
        public CanvasGroup CanvasGroup;

        private void OnEnable()
        {
            SetBackgroundColorAlpha();
            StartCoroutine(FadeInAfter());
        }

        private void OnDisable()
        {
            SetBackgroundColorAlpha();
        }

        private IEnumerator FadeInAfter()
        {
            yield return new WaitForSeconds(StartAnimationAfter);
            CanvasGroup.DOFade(1f, AnimationDuration);
        }

        private void SetBackgroundColorAlpha()
        {
            CanvasGroup.alpha = 0;
        }

    }
}