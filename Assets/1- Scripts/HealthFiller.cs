using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFiller : MonoBehaviour
{

     Slider Healthbar;
    public Gradient ChangingColor;
    public Image HealhbarImage;
    public GameObject Maincamera;

    private void Awake()
    {
        if (GamePlayUI.Instance)
        {
            //this.gameObject.transform.LookAt(GamePlayUI.Instance.Maincamera.transform);
        }

        Healthbar = GetComponent<Slider>();
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setMAXHealth(float health)
    {
        Healthbar.maxValue = health;
        Healthbar.value = health;

       HealhbarImage.color = ChangingColor.Evaluate(1f);
    }

    public void SetHealth(float CurrentHealth)
    {
        this.gameObject.SetActive(true);

        Healthbar.value = CurrentHealth;
        HealhbarImage.color = ChangingColor.Evaluate(Healthbar.normalizedValue);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
