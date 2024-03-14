using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace hardartcore.CasualGUI
{

    public class Dialog : MonoBehaviour
    {
        public float AnimDuration = 0.2f;
        public GameObject DialogContent;
        public TextMeshProUGUI gameVersionText;

        public void ShowDialog()
        {
            DialogContent.SetActive(false);
            gameObject.SetActive(true);
            DialogContent.transform.localScale = Vector3.zero;
            DialogContent.SetActive(true);
            DialogContent.transform.DOScale(Vector3.one, AnimDuration);
            gameVersionText.text = Application.version;
        }

        public void HideDialog()
        {
            DialogContent.transform.DOScale(Vector3.zero, AnimDuration);
            StartCoroutine(HideDialogAfterTime());
        }

        private IEnumerator HideDialogAfterTime()
        {
            yield return new WaitForSeconds(AnimDuration);
            gameObject.SetActive(false);
        }

         
    }
}
