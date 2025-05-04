using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passiveMoneyTimer_script : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public bool SliderBool = true;
    
    public void UpdatePassiveMoneyTimeBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    public void DisableSelfSlider()
    {
        slider.gameObject.SetActive(false);
    }
    public void ActivateSelfSlider()
    {
        slider.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
