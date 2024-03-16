using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    [SerializeField] Slider loadingSlider;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] Image gameStudioLogo;
    [SerializeField] float loadingTime = 2.0f;
    [SerializeField] float logoFillSpeed = 0.02f;


    float currentValue;
    void Update()
    {
        gameStudioLogo.fillAmount += Time.smoothDeltaTime * logoFillSpeed;
        currentValue += Time.deltaTime / loadingTime;
        loadingSlider.value = Mathf.Clamp01(currentValue);
        loadingText.text = ("Loading...")+ (int)(loadingSlider.value * 100)+ "%";

        if(loadingSlider.value >= 1)
        {
            SceneManager.LoadScene("Main Menu");
        }

    }
}
